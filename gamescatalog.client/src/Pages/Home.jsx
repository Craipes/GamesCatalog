import React, { useState, useEffect } from 'react';
import Filters from '@/Comp/Filters';
import GameList from '@/Comp/GameList';

function Home({ searchQuery }) {
  const [url, setUrl] = useState('https://localhost:7200/api/App/search');

  const applyFilters = (selectedFilters) => {
    const queryParams = new URLSearchParams();

    // Додаємо пошук за назвою, якщо він присутній
    if (searchQuery) queryParams.append('search', searchQuery);

    // Обробляємо параметри фільтрації
    Object.keys(selectedFilters).forEach(key => {
      const value = selectedFilters[key];
      if (Array.isArray(value) && value.length > 0) {
        // Об'єднуємо масиви значень (наприклад, теги) через кому
        queryParams.append(key, value.join(','));
      } else if (!Array.isArray(value) && value !== null && value !== '') {
        queryParams.append(key, value);
      }
    });

    // Оновлюємо URL залежно від наявності параметрів
    const newUrl = queryParams.toString()
      ? `https://localhost:7200/api/App/search?${queryParams.toString()}`
      : 'https://localhost:7200/api/App/search';

    setUrl(newUrl);
  };

  // Викликаємо applyFilters при зміні searchQuery
  useEffect(() => {
    applyFilters({});
  }, [searchQuery]);

  return (
    <div className='flex mx-3'>
      <Filters onApplyFilters={applyFilters} className='' />
      <GameList url={url} className=' ' />
    </div>
  );
}

export default Home;
