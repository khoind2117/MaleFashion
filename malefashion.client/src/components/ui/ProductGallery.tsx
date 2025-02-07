import { Box, IconButton, Modal } from "@mui/material";
import { useState } from "react";
import CloseIcon from '@mui/icons-material/Close';

const ProductGallery = () => {
    const [activeImage, setActiveImage] = useState("/src/assets/img/client/shop-details/product-big-2.png");
    const [isVideoOpen, setIsVideoOpen] = useState(false);

    const thumbnails = [
        { id: 1, image: "/src/assets/img/client/shop-details/thumb-1.png", bigImage: "/src/assets/img/client/shop-details/product-big-2.png" },
        { id: 2, image: "/src/assets/img/client/shop-details/thumb-2.png", bigImage: "/src/assets/img/client/shop-details/product-big-3.png" },
        { id: 3, image: "/src/assets/img/client/shop-details/thumb-3.png", bigImage: "/src/assets/img/client/shop-details/product-big.png" },
        { id: 4, image: "/src/assets/img/client/shop-details/thumb-4.png", bigImage: "/src/assets/img/client/shop-details/product-big-4.png", isVideo: true, videoUrl: "https://www.youtube.com/watch?v=8PJ3_p7VqHw&list=RD8PJ3_p7VqHw&start_radio=1" },
    ];

    const getVideoId = (url: string | undefined) => {
        if (!url) return "";
        const match = url.match(/(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^/]+\/.+\/|(?:v|e(?:mbed)?)\/|(?:.*[?&]v=))|youtu\.be\/)([a-zA-Z0-9_-]{11})/);
        return match ? match[1] : "";
      };

    const handleVideoOpen = (videoUrl: string) => {
        setIsVideoOpen(true);
    };

    const handleVideoClose = () => {
        setIsVideoOpen(false);
    };

    return (
        <div className="row">
            {/* Thumbnail List */}
            <div className="col-lg-3 col-md-3">
                <ul className="nav nav-tabs" role="tablist">
                    {thumbnails.map((thumb) => (
                        <li className="nav-item" key={thumb.id}>
                            <button
                                className={`nav-link ${activeImage === thumb.bigImage ? "active" : ""}`}
                                onClick={() => setActiveImage(thumb.bigImage)}
                                role="tab"
                                aria-selected={activeImage === thumb.bigImage}
                            >
                                <div
                                    className="product__thumb__pic"
                                    style={{ backgroundImage: `url(${thumb.image})` }}
                                >
                                    {thumb.isVideo && <i className="fa fa-play"></i>}
                                </div>
                            </button>
                        </li>
                    ))}
                </ul>
            </div>

            {/* Main Image */}
            <div className="col-lg-6 col-md-9">
                <div className="tab-content">
                    <div className="tab-pane active" role="tabpanel">
                        <div className="product__details__pic__item">
                            <img src={activeImage} alt="Product Details" />
                            {thumbnails.find((thumb) => thumb.bigImage === activeImage)?.isVideo && (
                                <a
                                onClick={() => handleVideoOpen(thumbnails.find((thumb) => thumb.bigImage === activeImage)?.videoUrl || "")}
                                className="video-popup"
                                style={{ cursor: "pointer" }}
                                >
                                <i className="fa fa-play" />
                                </a>
                            )}
                        </div>
                    </div>
                </div>
            </div>

            {/* Video Modal */}
            <Modal
                open={isVideoOpen}
                onClose={handleVideoClose}
                aria-labelledby="video-modal"
                aria-describedby="video-modal-description"
            >
                <Box
                sx={{
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    transform: "translate(-50%, -50%)",
                    width: "80%",
                    maxWidth: 800,
                    bgcolor: "background.paper",
                    borderRadius: 2,
                    boxShadow: 24,
                    p: 4,
                    overflow: "hidden",
                }}
                >
                {/* Modal Close Button */}
                <IconButton
                    sx={{ position: "absolute", top: 1, right: 1 }}
                    onClick={handleVideoClose}
                >
                    <CloseIcon />
                </IconButton>

                {/* Video */}
                <iframe
                    width="100%"
                    height="500"
                    src={`https://www.youtube.com/embed/${getVideoId(thumbnails.find((thumb) => thumb.bigImage === activeImage)?.videoUrl)}`}
                    title="YouTube video"
                    allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                    allowFullScreen
                />
                </Box>
            </Modal>
        </div>
    );
};

export default ProductGallery;
