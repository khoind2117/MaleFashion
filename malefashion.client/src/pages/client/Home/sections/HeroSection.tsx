import { useEffect } from "react";
import Slider from 'react-slick';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import SliderCustomArrow from "../../../../components/ui/SliderCustomArrow";
import { Link } from "react-router";

const Hero_Section = () => {
    useEffect(() => {
        const elements = document.querySelectorAll(".set-bg");
        elements.forEach((el) => {
            const element = el as HTMLElement;
            const bg = element.dataset.setbg;
            if (bg) {
                element.style.backgroundImage = `url(${bg})`;
            }
        });
    }, []);

    const settings = {
        fade: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        nextArrow: <SliderCustomArrow direction="next" />,
        prevArrow: <SliderCustomArrow direction="prev" />
    };

    return (
        // Hero Section Begin
        <section className="hero">
            <Slider {...settings} className="hero__slider">
                <div className="hero__items set-bg" data-setbg="/src/assets/img/client/hero/hero-1.jpg">
                    <div className="container">
                        <div className="row">
                            <div className="col-xl-5 col-lg-7 col-md-8">
                                <div className="hero__text" >
                                    <h6>Summer Collection</h6>
                                    <h2>Fall - Winter Collections 2030</h2>
                                    <p>
                                        A specialist label creating luxury essentials. Ethically
                                        crafted with an unwavering commitment to exceptional
                                        quality.
                                    </p>
                                    <Link to="/shop" className="primary-btn">
                                        Shop now <span className="arrow_right"></span>
                                    </Link>
                                    <div className="hero__social">
                                        <a href="#"><i className="fa fa-facebook"></i></a>
                                        <a href="#"><i className="fa fa-twitter"></i></a>
                                        <a href="#"><i className="fa fa-pinterest"></i></a>
                                        <a href="#"><i className="fa fa-instagram"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="hero__items set-bg" data-setbg="/src/assets/img/client/hero/hero-2.jpg">
                    <div className="container">
                        <div className="row">
                            <div className="col-xl-5 col-lg-7 col-md-8">
                                <div className="hero__text">
                                    <h6>Summer Collection</h6>
                                    <h2>Fall - Winter Collections 2030</h2>
                                    <p>
                                        A specialist label creating luxury essentials. Ethically
                                        crafted with an unwavering commitment to exceptional
                                        quality.
                                    </p>
                                    <a href="#" className="primary-btn">
                                        Shop now <span className="arrow_right"></span>
                                    </a>
                                    <div className="hero__social">
                                        <a href="#"><i className="fa fa-facebook"></i></a>
                                        <a href="#"><i className="fa fa-twitter"></i></a>
                                        <a href="#"><i className="fa fa-pinterest"></i></a>
                                        <a href="#"><i className="fa fa-instagram"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Slider>
        </section>
        // Hero Section End
    );
};

export default Hero_Section;
