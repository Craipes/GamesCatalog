import React, { useEffect, useState } from 'react';
import ProductBox from '@/Comp/ProductBox';
import { ScrollArea } from '@/components/ui/scroll-area';

function GameList({ url }) {
  const [games, setGames] = useState([]);

  useEffect(() => {
    console.log("Запит на сервер за даними ігор:", url);
    fetch(url)
      .then(response => response.json())
      .then(data => setGames(data))
      .catch(error => console.error("Помилка завантаження ігор:", error));
  }, [url]);

  return (
    <ScrollArea className="h-full rounded-md p-4">
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
      {games.length > 0 ? (
        games.map((game, index) => (
          <ProductBox key={index} game={game} />
        ))
      ) : (
        <p>Ігри не знайдені</p>
      )}
    </div>
    </ScrollArea>
  );
}

export default GameList;
