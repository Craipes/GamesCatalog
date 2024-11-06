import React, { useEffect, useState } from 'react';
import ProductBox from '@/Comp/ProductBox';
import { ScrollArea } from '@/components/ui/scroll-area';
import TransitionsModal from './TransitionModal';
import { Button } from '@/components/ui/button';
import { Fade } from 'react-awesome-reveal';
import { Badge } from '@/components/ui/badge';

function GameList({ url, setCurrentPage }) {
  const [games, setGames] = useState([]);
  const [selectedGame, setSelectedGame] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentPage, setPage] = useState(1);
  const [hasNextPage, setHasNextPage] = useState(true);

  const gamesPerPage = 8;

  const fetchGames = (page) => {
    fetch(`${url}&page=${page}`)
      .then((response) => response.json())
      .then((data) => {
        setGames(data);
        setHasNextPage(data.length === gamesPerPage);
      })
      .catch((error) => console.error("Error loading games:", error));
  };

  useEffect(() => {
    fetchGames(currentPage);
  }, [url]);

  const handleGameClick = (gameId) => {
    fetch(`https://localhost:7200/api/App/game/${gameId}`)
      .then((response) => response.json())
      .then((data) => {
        setSelectedGame(data);
        setIsModalOpen(true);
      })
      .catch((error) => console.error("Error loading game details:", error));
  };

  const handleNextPage = () => {
    if (hasNextPage) {
      setPage((prev) => prev + 1);
      setCurrentPage((prev) => prev + 1); 
    }
  };

  const handlePreviousPage = () => {
    if (currentPage > 1) {
      setPage((prev) => prev - 1);
      setCurrentPage((prev) => prev - 1); 
    }
  };

  return (
    <>
      <ScrollArea className="h-screen rounded-md pl-4">
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 min-[1350px]:grid-cols-4 gap-6 px-6 pt-4">
          {games.length > 0 ? (
            games.map((game, index) => (
              <Fade key={index}>
                <ProductBox game={game} onClick={() => handleGameClick(game.id)} />
              </Fade>
            ))
          ) : (
            <p>Нічого не знайдено :(</p>
          )}
        </div>
        <div className="flex justify-center space-x-4 mt-4">
          <Button onClick={handlePreviousPage} disabled={currentPage === 1}>
            Попередня
          </Button>
          <Button className='cursor-default'>{currentPage}</Button>
          <Button onClick={handleNextPage} disabled={!hasNextPage}>
            Наступна
          </Button>
        </div>
        <div className='mb-24'></div>
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
