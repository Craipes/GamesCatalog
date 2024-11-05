import React, { useEffect, useState } from 'react'; 
import ProductBox from '@/Comp/ProductBox';
import { ScrollArea } from '@/components/ui/scroll-area';
import TransitionsModal from './TransitionModal';
import {Fade} from 'react-awesome-reveal'

function GameList({ url }) {
  const [games, setGames] = useState([]);
  const [selectedGame, setSelectedGame] = useState(null); 
  const [isModalOpen, setIsModalOpen] = useState(false); 

  useEffect(() => {
    fetch(url)
      .then(response => response.json())
      .then(data => setGames(data))
      .catch(error => console.error("Error loading games:", error));
  }, [url]);


  const handleGameClick = (gameId) => {
    console.log('Game clicked:', gameId);
    fetch(`https://localhost:7200/api/App/game/${gameId}`)
      .then(response => response.json())
      .then(data => {
        setSelectedGame(data);
        setIsModalOpen(true);
      })
      .catch(error => console.error("Error loading game details:", error));
  };

  return (
    <>
      <ScrollArea className="h-screen rounded-md pl-4">
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 px-6 pt-4">
          {games.length > 0 ? (
            games.map((game, index) => (
              <Fade>
              <ProductBox key={index} game={game} onClick={() => handleGameClick(game.id)} />
              </Fade>
            ))
          ) : (
            <p>Нічого не знайдено :(</p>
          )}
        </div>
      </ScrollArea>

      {selectedGame && (
        <TransitionsModal 
          open={isModalOpen} 
          handleClose={() => setIsModalOpen(false)} 
          game={selectedGame} 
        />
      )}
    </>
  );
}

export default GameList;
