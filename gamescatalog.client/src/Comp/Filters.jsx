import React, { useEffect, useState } from 'react';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Checkbox } from '@/components/ui/checkbox';
import { Switch } from '@/components/ui/switch';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Label } from '@radix-ui/react-label';
import { Button } from '@/components/ui/button';
import Slider from '@mui/material/Slider';
import { createTheme } from '@mui/material/styles';
import {Fade} from 'react-awesome-reveal'

function Filters({ onApplyFilters }) {
  const initialFilters = {
    tags: [],
    platforms: [],
    catalogs: [],
    developers: [],
    publishers: [],
    ordering: 0,
    minRating: 0,
    maxRating: 100,
    minYear: 1980,
    maxYear: 2024,
    dlc: null,
    minPrice: 0,
    maxPrice: 1000,
    isReleased: true,
    indexDLCs: true,
    gamesPerPage: 12,
    page: 1
  };

  const theme = createTheme({
    palette: {
      ochre: {
        main: '#E3D026',
        light: '#E9DB5D',
        dark: '#A29415',
        contrastText: '#242105',
      },
    },
  });

  const [filters, setFilters] = useState({
    tags: [],
    platforms: [],
    catalogs: [],
    developers: [],
    publishers: []
  });

  const [selectedFilters, setSelectedFilters] = useState(initialFilters);

  useEffect(() => {
    fetch('https://localhost:7200/api/App/filters')
      .then(response => response.json())
      .then(data => setFilters(data))
      .catch(error => console.error("Помилка завантаження даних:", error));
  }, []);

  const handleCheckboxChange = (filterType, id) => {
    setSelectedFilters(prevFilters => {
      const updatedList = prevFilters[filterType].includes(id)
        ? prevFilters[filterType].filter(item => item !== id)
        : [...prevFilters[filterType], id];

      return { ...prevFilters, [filterType]: updatedList };
    });
  };

  const handleRadioChange = (key, value) => {
    setSelectedFilters(prevFilters => ({
      ...prevFilters,
      [key]: value
    }));
  };

  const handleApplyFilters = () => {
    onApplyFilters(selectedFilters);
  };

  const handleResetFilters = () => {
    setSelectedFilters(initialFilters).then(() => onApplyFilters(selectedFilters));
    
  };

  const handleRatingChange = (event, newValue) => {
    setSelectedFilters(prevFilters => ({
      ...prevFilters,
      minRating: newValue[0],
      maxRating: newValue[1]
    }));
  };

  const handlePriceChange = (event, newValue) => {
    setSelectedFilters(prevFilters => ({
      ...prevFilters,
      minPrice: newValue[0],
      maxPrice: newValue[1]
    }));
  };

  const handleYearChange = (event, newValue) => {
    setSelectedFilters(prevFilters => ({
      ...prevFilters,
      minYear: newValue[0],
      maxYear: newValue[1]
    }));
  };

  return (
    <div>
      <h2 className='text-[30px] font-bold'>Фільтри</h2>
     <ScrollArea className="h-[940px] w-[350px] rounded-md p-4">
        <div className='px-4'>
          {/* Сортування */}
          <section>
            <h3 className='text-[20px] font-bold'>Сортування</h3>
            <RadioGroup defaultValue="0">
              {['За замовчуванням', 'Назва зростання', 'Назва спадання', 'Рейтинг зростання', 'Рейтинг спадання', 'Рік зростання', 'Рік спадання'].map((label, index) => (
                <Fade>
                <div className="flex items-center space-x-2" key={index}>
                  <RadioGroupItem value={index.toString()} onClick={() => handleRadioChange('ordering', index)} />
                  <Label>{label}</Label>
                </div>
                </Fade>
              ))}
            </RadioGroup>
          </section>

          {/* DLC */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>DLC</h3>
            <RadioGroup defaultValue="2">
              {['Шукати всі', 'Тільки з DLC', 'Без DLC'].map((label, index) => (
                <Fade>
                <div className="flex items-center space-x-2" key={index}>
                  <RadioGroupItem value={index.toString()} onClick={() => handleRadioChange('dlc', index === 0 ? null : index === 1)} />
                  <Label>{label}</Label>
                </div>
                </Fade>
              ))}
            </RadioGroup>
          </section>

          {/* Вже випущена */}
          <section>
            <h3 className='text-[20px] font-bold mt-2'>Вже випущена</h3>
            <Fade>
            <Switch className='ml-2' checked={selectedFilters.isReleased} onCheckedChange={value => handleRadioChange('isReleased', value)} />
            </Fade>
          </section>

          {/* Показувати DLC разом з іграми */}
          <section>
            <h3 className='text-[20px] font-bold mt-2'>Показувати DLC разом з іграми</h3>
            <Fade>
            <Switch className='ml-2' checked={selectedFilters.indexDLCs} onCheckedChange={value => handleRadioChange('indexDLCs', value)} />
            </Fade>
          </section>

          {/* Ціна */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Ціна</h3>
            <Fade>
            <Slider
            color='theme'
              value={[selectedFilters.minPrice, selectedFilters.maxPrice]}
              onChange={handlePriceChange}
              min={0}
              max={1000}
              valueLabelDisplay="auto"
            />
            </Fade>
          </section>

          {/* Рейтинг */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Рейтинг</h3>
            <Fade>
            <Slider
            color='theme'
              value={[selectedFilters.minRating, selectedFilters.maxRating]}
              onChange={handleRatingChange}
              min={0}
              max={100}
              valueLabelDisplay="auto"
            />
            </Fade>
          </section>

          {/* Рік */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Рік</h3>
            <Fade>
            <Slider
            color='theme'
              value={[selectedFilters.minYear, selectedFilters.maxYear]}
              onChange={handleYearChange}
              min={1980}
              max={2024}
              valueLabelDisplay="auto"
            />
            </Fade>
          </section>

          {/* Теги */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Теги</h3>
            {filters.tags.map(tag => (
              <Fade>
              <div key={tag.id} className='flex items-center'>
                <Checkbox
                  checked={selectedFilters.tags.includes(tag.id)}
                  onCheckedChange={() => handleCheckboxChange('tags', tag.id)}
                  className='mr-2'
                />
                <Label>{tag.name}</Label>
              </div>
              </Fade>
            ))}
          </section>

          {/* Платформи */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Платформи</h3>
            {filters.platforms.map(platform => (
              <Fade>
              <div key={platform.id} className='flex items-center'>
                <Checkbox
                  checked={selectedFilters.platforms.includes(platform.id)}
                  onCheckedChange={() => handleCheckboxChange('platforms', platform.id)}
                  className='mr-2'
                />
                <Label>{platform.name}</Label>
              </div>
              </Fade>
            ))}
          </section>

          {/* Каталоги */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Каталоги</h3>
            {filters.catalogs.map(catalog => (
              <Fade>
              <div key={catalog.id} className='flex items-center'>
                <Checkbox
                  checked={selectedFilters.catalogs.includes(catalog.id)}
                  onCheckedChange={() => handleCheckboxChange('catalogs', catalog.id)}
                  className='mr-2'
                />
                <Label>{catalog.name}</Label>
              </div>
              </Fade>
            ))}
          </section>

          {/* Розробники */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Розробники</h3>
            {filters.developers.map(developer => (
              <Fade>
              <div key={developer.id} className='flex items-center'>
                <Checkbox
                  checked={selectedFilters.developers.includes(developer.id)}
                  onCheckedChange={() => handleCheckboxChange('developers', developer.id)}
                  className='mr-2'
                />
                <Label>{developer.name}</Label>
              </div>
              </Fade>
            ))}
          </section>

          {/* Видавці */}
          <section>
            <h3 className='text-[20px] font-bold mt-4'>Видавці</h3>
            {filters.publishers.map(publisher => (
              <Fade>
              <div key={publisher.id} className='flex items-center'>
                <Checkbox
                  checked={selectedFilters.publishers.includes(publisher.id)}
                  onCheckedChange={() => handleCheckboxChange('publishers', publisher.id)}
                  className='mr-2'
                />
                <Label>{publisher.name}</Label>
              </div>
              </Fade>
            ))}
          </section>
        </div>
      </ScrollArea>
      {/* Кнопки "Застосувати" та "Скинути" */}
  <Fade>
      <div className="flex justify-center space-x-4 mt-4">
        <Button className="rounded-xl" onClick={handleApplyFilters}>Застосувати</Button>
        <Button className="rounded-xl" onClick={handleResetFilters}>Скинути</Button>
      </div>
      </Fade>
    </div>
  );
}

export default Filters;
