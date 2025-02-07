import { Box, CircularProgress, Fade } from '@mui/material';
import { useEffect, useState } from 'react'

const Preloader = () => {
  const [loading, setLoading] = useState(true);   // Điều khiển việc ẩn/hiện của loader
  const [preloaderVisible, setPreloaderVisible] = useState(true); // Điều khiển preloader  
  
  useEffect(() => {
    setTimeout(() => {
      setLoading(false);

      setTimeout(() => {
        setPreloaderVisible(false);
      }, 200);
    }, 600);
  }, []);

  return (
    <>
      {preloaderVisible && (
        <Fade in={loading} timeout={600}>
          <Box id="preloder">
            <CircularProgress className="loader" />
          </Box>
        </Fade>
      )}
    </>
  );
};

export default Preloader