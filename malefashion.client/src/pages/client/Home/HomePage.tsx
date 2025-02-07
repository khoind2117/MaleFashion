import Preloader from "../Preloader";
import Hero_Section from "./sections/HeroSection";
import Banner_Section from "./sections/BannerSection";
import Product_Section from "./sections/ProductSection";
import Categories_Section from "./sections/CategoriesSection";
import Instagram_Section from "./sections/InstagramSection";
import LastestBlogSection from "./sections/LastestBlogSection";
import MainLayout from "../../../layouts/MainLayout";

const HomePage: React.FC = () => {
  return (
    <>
      <Preloader/>
      <MainLayout>
        <Hero_Section/>
        <Banner_Section/>
        <Product_Section/>
        <Categories_Section/>
        <Instagram_Section/>
        <LastestBlogSection/>
      </MainLayout>
    </>
  );
};

export default HomePage;
