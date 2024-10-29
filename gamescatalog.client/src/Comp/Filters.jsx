import React, { useEffect, useState } from 'react';
import { ScrollArea } from '@/components/ui/scroll-area';
import { Checkbox } from '@/components/ui/checkbox';
import { Switch } from '@/components/ui/switch';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Label } from '@radix-ui/react-label';
import { Button } from '@/components/ui/button';

function Filters({ onApplyFilters }) {
  const [filters, setFilters] = useState({
    tags: [],
    platforms: [],
    catalogs: [],
    developers: [],
    publishers: []
  });

  const [selectedFilters, setSelectedFilters] = useState({
    tags: [],
    platforms: [],
    catalogs: [],
    developers: [],
    publishers: [],
    ordering: 0,
    minRating: 0,
    maxRating: 100,
    minYear: 0,
    maxYear: 10000,
    dlc: null,
    minPrice: 0,
    maxPrice: 1000000,
    isReleased: true,
    indexDLCs: true,
    gamesPerPage: 12,
    page: 1
  });

  useEffect(() => {
    fetch('https://localhost:7200/api/App/filters')
      .then(response => response.json())
      .then(data => setFilters(data))
      .catch(error => console.error("Помилка завантаження даних:", error));
  }, []);

  // Функція для обробки вибору
  const handleCheckboxChange = (filterType, id) => {
    setSelectedFilters(prevFilters => {
      const updatedList = prevFilters[filterType].includes(id)
        ? prevFilters[filterType].filter(item => item !== id)
        : [...prevFilters[filterType], id];

      return { ...prevFilters, [filterType]: updatedList };
    });
  };

  const handleApplyFilters = () => {
    onApplyFilters(selectedFilters);
  };

  return (
    <div>
      <h2 className='text-[30px] font-bold'>Фільтри</h2>
<ScrollArea className="h-[940px] w-[350px] rounded-md p-4">
      <div className='px-4'>
        

        {/* Теги */}
        <section>
          <h3 className='text-[20px] font-bold'>Теги</h3>
          <ScrollArea className="h-[200px] w-[350px] rounded-md p-4">
            <ul>
              {filters.tags.map(tag => (
                <div key={tag.id} className='flex items-center'>
                  <Checkbox 
                    className='mr-2'
                    onCheckedChange={() => handleCheckboxChange('tags', tag.id)} 
                  />
                  <li>{tag.name}</li>
                </div>
              ))}
            </ul>
          </ScrollArea>
        </section>

        {/* Платформи */}
        <section>
          <h3 className='text-[20px] font-bold'>Платформи</h3>
          <ul>
            {filters.platforms.map(platform => (
              <div key={platform.id} className='flex items-center ml-4'>
                <Checkbox 
                  className='mr-2'
                  onCheckedChange={() => handleCheckboxChange('platforms', platform.id)} 
                />
                <li>{platform.name}</li>
              </div>
            ))}
          </ul>
        </section>

        {/* Каталоги */}
        <section>
          <h3 className='text-[20px] font-bold'>Каталоги</h3>
          <ul>
            {filters.catalogs.map(catalog => (
              <div key={catalog.id} className='flex items-center ml-4'>
                <Checkbox 
                  className='mr-2'
                  onCheckedChange={() => handleCheckboxChange('catalogs', catalog.id)} 
                />
                <li>{catalog.name}</li>
              </div>
            ))}
          </ul>
        </section>

        {/* Розробники */}
        <section>
          <h3 className='text-[20px] font-bold'>Розробники</h3>
          <ul>
            {filters.developers.map(developer => (
              <div key={developer.id} className='flex items-center ml-4'>
                <Checkbox 
                  className='mr-2'
                  onCheckedChange={() => handleCheckboxChange('developers', developer.id)} 
                />
                <li>{developer.name}</li>
              </div>
            ))}
          </ul>
        </section>

        {/* Видавці */}
        <section>
          <h3 className='text-[20px] font-bold'>Видавці</h3>
          <ul>
            {filters.publishers.map(publisher => (
              <div key={publisher.id} className='flex items-center ml-4'>
                <Checkbox 
                  className='mr-2'
                  onCheckedChange={() => handleCheckboxChange('publishers', publisher.id)} 
                />
                <li>{publisher.name}</li>
              </div>
            ))}
          </ul>
        </section>

      
      </div>

    </ScrollArea>
    <Button className="rounded-xl mx-auto" onClick={handleApplyFilters}>Застосувати</Button>
    </div>

  );
}

export default Filters;
