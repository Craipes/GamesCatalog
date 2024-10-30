import React from 'react';
import Backdrop from '@mui/material/Backdrop';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Typography from '@mui/material/Typography';
import { CarouselNext, CarouselPrevious, CarouselItem, CarouselContent, Carousel } from '@/components/ui/carousel';

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 1000,
  bgcolor: 'background.paper',
  boxShadow: 24,
  p: 4,
};

export default function TransitionsModal({ open, handleClose, game }) {
  if (!game) return null;

  return (
    <Modal
      style={{  borderRadius: '8px' }}
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
        <Box sx={style}>
          <Typography sx={{ mt: 2 }}>
            <Carousel>
              <CarouselContent>
                {game.contentUrls.map((url, index) => (
                  <CarouselItem className="basis-1/2" key={index}>
                    <img
                      src={url}
                      alt={`${game.title} image ${index + 1}`}
                      style={{ width: '500px', height: '400px', objectFit: 'cover', borderRadius: '8px' }}
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
            <strong>Release Year:</strong> {game.yearOfRelease} <br />
            <strong>Rating:</strong> ({game.rating}/100) <br />
            <strong>Price:</strong> ${game.price} <br />
            <strong>Description:</strong> {game.description} <br />
            <strong>Requirements:</strong> {game.requirements} <br />
            <strong>Developer:</strong> {game.developer} <br />
            <strong>Publisher:</strong> {game.publisher} <br />
          </Typography>

          {/* Tags */}
          <Typography sx={{ mt: 3 }}>
            <strong>Tags:</strong> {game.tags.join(', ')}
          </Typography>

          {/* Platforms */}
          <Typography sx={{ mt: 2 }}>
            <strong>Platforms:</strong> {game.platforms.join(', ')}
          </Typography>

          {/* Catalog Links */}
          <Typography sx={{ mt: 2 }}>
            <strong>Available in Catalogs:</strong> 
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
        </Box>
      </Fade>
    </Modal>
  );
}
