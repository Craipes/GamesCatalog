import React, { useEffect, useState } from 'react';
import {ScrollArea } from '@/components/ui/scroll-area';
import {Checkbox}  from '@/components/ui/checkbox';
import { Switch } from '@/components/ui/switch';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Label } from '@radix-ui/react-label';
import { Button } from '@/components/ui/button';


function Filters() {
  const [filters, setFilters] = useState({
    tags: [],
    platforms: [],
    catalogs: [],
    developers: [],
    publishers: [],
  });

  useEffect(() => {
    fetch('https://localhost:7200/api/App/filters')
      .then(response => response.json())
      .then(data => setFilters(data))
      .catch(error => console.error("Помилка завантаження даних:", error));
  }, []);



  return (
    <div className='px-4'>
      <h2 className='text-[30px] font-bold '>Фільтри</h2>

      <section>
        <h3 className='text-[20px] font-bold '>Теги</h3>
        <ScrollArea className="h-[200px] w-[350px] rounded-md p-4">
        <ul>
          {filters.tags.map(tag => (
            <div key={tag.id} className='flex items-center '>
                <Checkbox className='mr-2'/>
                <li key={tag.id}>{tag.name}</li>
            </div>
          ))}
        </ul>
        </ScrollArea>
      </section>

      <section>
        <h3 className='text-[20px] font-bold '>Платформи</h3>
        <ul>
          {filters.platforms.map(platform => (
                 <div key={platform.id} className='flex items-center ml-4'>
                 <Checkbox className='mr-2'/>
                 <li key={platform.id}>{platform.name}</li>
             </div>
          ))}
        </ul>
      </section>

      <section>
        <h3 className='text-[20px] font-bold '>Каталоги</h3>
        <ul>
          {filters.catalogs.map(catalog => (
                 <div key={catalog.id} className='flex items-center ml-4'>
                 <Checkbox className='mr-2'/>
                 <li key={catalog.id}>{catalog.name}</li>
             </div>
          ))}
        </ul>
      </section>

      <section>
        <h3 className='text-[20px] font-bold '>Розробники</h3>
        <ul>
          {filters.developers.map(developer => (
                 <div key={developer.id} className='flex items-center ml-4'>
                 <Checkbox className='mr-2'/>
                 <li key={developer.id}>{developer.name}</li>
             </div>
          ))}
        </ul>
      </section>

      <section>
        <h3 className='text-[20px] font-bold '>Видавці</h3>
        <ul>
          {filters.publishers.map(publisher => (
                 <div key={publisher.id} className='flex items-center ml-4'>
                 <Checkbox className='mr-2'/>
                 <li key={publisher.id}>{publisher.name}</li>
             </div>
          ))}
        </ul>
      </section>

      <section>
      <h3 className='text-[20px] font-bold '>Сортування</h3>
          <RadioGroup defaultValue="0">
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="0" id="0" />
          <Label htmlFor="0">За замовчуванням</Label>
      </div>
      <div className="flex items-center space-x-2">
        <RadioGroupItem value="1" id="1" />
        <Label htmlFor="1">Назва зростання</Label>
      </div>
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="2" id="2" />
          <Label htmlFor="2">Назва спадання</Label>
      </div>
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="3" id="3" />
          <Label htmlFor="3">Рейтинг зростання</Label>
      </div>
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="4" id="4" />
          <Label htmlFor="4">Рейтинг спадання</Label>
      </div>
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="5" id="5" />
          <Label htmlFor="5">Рік зростання</Label>
      </div>
      <div className="flex items-center space-x-2">
          <RadioGroupItem value="6" id="6" />
          <Label htmlFor="6">Рік спадання</Label>
      </div>
    </RadioGroup>
      </section>

      <section>
      <h3 className='text-[20px] font-bold '>Рейтинг</h3>
      <div className="w-full px-10">

    </div>
      </section>

      <section>
      <h3 className='text-[20px] font-bold '>Рік</h3>
      </section>

      <section>
          <div className='flex items-center'>
            <h3 className='text-[20px] font-bold '>DLC</h3>
            <Switch className='ml-2'/>
          </div>
      </section>

      <section>
      <h3 className='text-[20px] font-bold '>Ціна</h3>
      </section>

      <section>
      <div className='flex items-center'>
            <h3 className='text-[20px] font-bold '>Вже випущена</h3>
            <Switch className='ml-2'/>
          </div>
      </section>
      <div>
        <Button>Застосувати</Button>
      </div>

    </div>
  );
}

export default Filters;