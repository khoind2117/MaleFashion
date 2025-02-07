import React, { useState } from 'react';
import { Modal, Box, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

const VideoPopup = () => {
  const [isOpen, setIsOpen] = useState(false);

  const openModal = () => setIsOpen(true);
  const closeModal = () => setIsOpen(false);

  return (
    <div>
      {/* Hình ảnh đại diện cho video */}
      <img
        src="img/shop-details/product-big-4.png"
        alt="Product Image"
        onClick={openModal}
        style={{ cursor: 'pointer' }}
      />
      
      {/* MUI Modal để hiển thị video */}
      <Modal
        open={isOpen}
        onClose={closeModal}
        aria-labelledby="video-modal"
        aria-describedby="video-modal-description"
      >
        <Box
          sx={{
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            width: '80%',
            maxWidth: 800,
            bgcolor: 'background.paper',
            borderRadius: 2,
            boxShadow: 24,
            p: 4,
            overflow: 'hidden',
          }}
        >
          {/* Nút đóng Modal */}
          <IconButton
            sx={{ position: 'absolute', top: 10, right: 10 }}
            onClick={closeModal}
          >
            <CloseIcon />
          </IconButton>

          {/* Video YouTube */}
          <iframe
            width="100%"
            height="500"
            src="https://www.youtube.com/watch?v=8PJ3_p7VqHw&list=RD8PJ3_p7VqHw&start_radio=1"
            title="YouTube video"
            allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
            allowFullScreen
          />
        </Box>
      </Modal>
    </div>
  );
};

export default VideoPopup;
