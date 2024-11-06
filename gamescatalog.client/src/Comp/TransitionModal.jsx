import React from 'react';
import Backdrop from '@mui/material/Backdrop';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Typography from '@mui/material/Typography';
import useMediaQuery from '@mui/material/useMediaQuery';
import { CarouselNext, CarouselPrevious, CarouselItem, CarouselContent, Carousel } from '@/components/ui/carousel';
import { ScrollArea } from '@/components/ui/scroll-area';

export default function TransitionsModal({ open, handleClose, game }) {
  if (!game) return null;

  const isSmallScreen = useMediaQuery('(max-width:600px)');
  const isMediumScreen = useMediaQuery('(max-width:900px)');


  const modalStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: isSmallScreen ? '90%' : isMediumScreen ? '80%' : 1000,
    height: '80vh',
    bgcolor: 'background.paper',
    boxShadow: 24,
    p: 4,
    borderRadius: '8px',
  };

  return (
    <Modal
      aria-labelledby="game-modal-title"
      aria-describedby="game-modal-description"
      open={open}
      onClose={handleClose}
      closeAfterTransition
      slots={{ backdrop: Backdrop }}
      slotProps={{
        backdrop: { timeout: 500 },
      }}
    >
      <Fade in={open}>
        <Box sx={modalStyle} className='flex flex-col'>
          <Typography sx={{ mt: 2, mb: 2 }}>
            <Carousel>
              <CarouselContent>
                {game.contentUrls.map((url, index) => (
                  <CarouselItem className="basis-1/2" key={index}>
                    <img
                      src={url}
                      alt={`${game.title} image ${index + 1}`}
                      style={{
                        width: isSmallScreen ? '100%' : '500px',
                        height: isSmallScreen ? '250px' : '400px',
                        objectFit: 'cover',
                        borderRadius: '8px',
                      }}
                    />
                  </CarouselItem>
                ))}
              </CarouselContent>
              {game.contentUrls.length > 1 && (
                <>
                  <CarouselPrevious />
                  <CarouselNext />
                </>
              )}
            </Carousel>
          </Typography>
          <ScrollArea className='flex-1'>
            <Typography id="game-modal-title" variant="h4" component="h2" gutterBottom>
              {game.title}
            </Typography>
            <Typography id="game-modal-description" sx={{ mt: 2 }}>
              <strong>Рік випуску:</strong> {game.yearOfRelease} <br />
              <strong>Рейтинг:</strong> ({game.rating}/100) <br />
              <strong>Ціна:</strong> ${game.price} <br />
              <strong>Опис:</strong> {game.description} <br />
              <strong>Вимоги:</strong> {game.requirements} <br />
              <strong>Розробник:</strong> {game.developer} <br />
              <strong>Видавець:</strong> {game.publisher} <br />
            </Typography>

            {/* Tags */}
            <Typography sx={{ mt: 3 }}>
              <strong>Теги:</strong> {game.tags.join(', ')}
            </Typography>

            {/* Platforms */}
            <Typography sx={{ mt: 2 }}>
              <strong>Платформи:</strong> {game.platforms.join(', ')}
            </Typography>

            {/* Catalog Links */}
            <Typography sx={{ mt: 2 }}>
              <strong>Доступно на:</strong>
              <ul>
                {game.catalogsLinks.map((catalog, index) => (
                  <li key={index}>
                    <a href={catalog.url} target="_blank" rel="noopener noreferrer">
                      {catalog.title || 'Link'}
                    </a>
                  </li>
                ))}
              </ul>
            </Typography>
            {game.dlCs.length > 0 && 
             <Typography>
             <strong>DLCs:</strong> {game.dlCs.map((game) => game.title).join(', ')}
             {game.dlCs.map((game) =>
             (
               <div>
                 <Typography sx={{ mt: 2, mb: 2 }}>
                   <Carousel>
                     <CarouselContent>
                       {game.contentUrls.map((url, index) => (
                         <CarouselItem className="basis-1/2" key={index}>
                           <img
                             src={url}
                             alt={`${game.title} image ${index + 1}`}
                             style={{
                               width: isSmallScreen ? '100%' : '500px',
                               height: isSmallScreen ? '250px' : '400px',
                               objectFit: 'cover',
                               borderRadius: '8px',
                             }}
                           />
                         </CarouselItem>
                       ))}
                     </CarouselContent>
                     {game.contentUrls.length > 1 && (
                       <>
                         <CarouselPrevious />
                         <CarouselNext />
                       </>
                     )}
                   </Carousel>
                 </Typography>
                 <Typography id="game-modal-title" variant="h4" component="h2" gutterBottom>
                   {game.title}
                 </Typography>
                 <Typography id="game-modal-description" sx={{ mt: 2 }}>
                   <strong>Рік випуску:</strong> {game.yearOfRelease} <br />
                   <strong>Рейтинг:</strong> ({game.rating}/100) <br />
                   <strong>Ціна:</strong> ${game.price} <br />
                   <strong>Опис:</strong> {game.description} <br />
                   <strong>Вимоги:</strong> {game.requirements} <br />
                   <strong>Розробник:</strong> {game.developer} <br />
                   <strong>Видавець:</strong> {game.publisher} <br />
                 </Typography>

                 {/* Tags */}
                 <Typography sx={{ mt: 3 }}>
                   <strong>Теги:</strong> {game.tags.join(', ')}
                 </Typography>

                 {/* Platforms */}
                 <Typography sx={{ mt: 2 }}>
                   <strong>Платформи:</strong> {game.platforms.join(', ')}
                 </Typography>

                 {/* Catalog Links */}
                 <Typography sx={{ mt: 2 }}>
                   <strong>Доступно на:</strong>
                   <ul>
                     {game.catalogsLinks.map((catalog, index) => (
                       <li key={index}>
                         <a href={catalog.url} target="_blank" rel="noopener noreferrer">
                           {catalog.title || 'Link'}
                         </a>
                       </li>
                     ))}
                   </ul>
                 </Typography>
               </div>
             )
             )}
           </Typography>
            }
           
          </ScrollArea>
        </Box>
      </Fade>
    </Modal>
  );
}
