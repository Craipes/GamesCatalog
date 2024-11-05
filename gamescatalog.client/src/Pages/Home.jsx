import React, { useState, useEffect } from 'react';
import Filters from '@/Comp/Filters';
import GameList from '@/Comp/GameList';
import { FiMenu, FiArrowLeft } from 'react-icons/fi';

function Home({ searchQuery }) {
  const [url, setUrl] = useState('https://localhost:7200/api/App/search');
  const [isFiltersOpen, setIsFiltersOpen] = useState(false);

  const applyFilters = (selectedFilters) => {
    const queryParams = new URLSearchParams();

    if (searchQuery) queryParams.append('search', searchQuery);

    Object.keys(selectedFilters).forEach(key => {
      const value = selectedFilters[key];
      if (Array.isArray(value) && value.length > 0) {
        queryParams.append(key, value.join(','));
      } else if (!Array.isArray(value) && value !== null && value !== '') {
        queryParams.append(key, value);
      }
    });

    const newUrl = queryParams.toString()
      ? `https://localhost:7200/api/App/search?${queryParams.toString()}`
      : 'https://localhost:7200/api/App/search';

    setUrl(newUrl);
  };

  useEffect(() => {
    applyFilters({});
  }, [searchQuery]);

  return (
    <div className="mx-3">
      {isFiltersOpen && (
        <div 
          className="fixed inset-0 z-10 flex justify-start items-center bg-black bg-opacity-50"
          onClick={() => setIsFiltersOpen(false)} 
        >
          <div 
            className="bg-white w-11/12 max-w-md p-4 rounded-lg shadow-lg overflow-y-auto"
            onClick={(e) => e.stopPropagation()} 
          >
            <button
              className="mb-4 text-gray-600"
              onClick={() => setIsFiltersOpen(false)}
            >
              <FiArrowLeft size={24} />
            </button>
            <Filters onApplyFilters={applyFilters} />
          </div>
        </div>
      )}

      <div className="flex">
        <button
          className="block min-[1100px]:hidden p-2 text-gray-600"
          onClick={() => setIsFiltersOpen(true)}
        >
          <FiMenu size={24} />
        </button>

        <div className="hidden min-[1100px]:block">
          <Filters onApplyFilters={applyFilters} />
        </div>

        <GameList url={url} className="w-full" />
      </div>
    </div>
  );
}

export default Home;
