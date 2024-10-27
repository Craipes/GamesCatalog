import Filters from '@/Comp/Filters'
import GameList from '@/Comp/GameList'
import React, {useState} from 'react'

function Home() {
  const [filters, setFilters] = useState(null);

  const applyFilters = (selectedFilters) => {
    setFilters(selectedFilters);
  };

  return (
    <div>
      <Filters/>
      <GameList/>
    </div>
  );
}

export default Home