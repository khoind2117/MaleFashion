import React from 'react'
import Header from './Header';
import Footer from './Footer';
import Offcanvas_Menu from './Offcanvas_Menu';

interface MainLayoutProps {
    children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({children}) => {
  return (
    <>
        <Offcanvas_Menu/>
        <Header/>
        <main>{children}</main>
        <Footer/>
    </>
  )
}

export default MainLayout