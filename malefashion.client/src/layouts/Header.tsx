import { Link, useNavigate } from "react-router-dom"
import HeaderMenu from "../components/ui/HeaderMenu"
import { useSelector } from "react-redux";
import { CartItem } from "../models/dtos/CartItem/CartItem";
import { RootState } from "../store/store";
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

const Header = () => {
    const { user, logout } = useContext(AuthContext) ?? {};
    const navigate = useNavigate();
    
    const cartItems = useSelector((state: RootState) => state.cart.items);
    
    const cartTotal = cartItems.reduce((sum: number, item: CartItem) => sum + item.quantity * item.unitPrice, 0);

    return (
        // Header Section Begin
        <header className="header">
            <div className="header__top">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-6 col-md-7">
                            <div className="header__top__left">
                                <p>Free shipping, 30-day return or refund guarantee.</p>
                            </div>
                        </div>
                        <div className="col-lg-6 col-md-5">
                            <div className="header__top__right">
                                <div className="header__top__links">
                                    {user ? (
                                        <>
                                            <span>Welcome, {user.userName}!</span>
                                            <button onClick={logout} className="logout-btn">Logout</button>
                                        </>
                                    ) : (
                                        <a href="#" onClick={() => navigate("/login")}>Login</a>
                                    )}
                                    <a href="#">FAQs</a>
                                </div>
                                <div className="header__top__hover">
                                    <span>Usd <i className="arrow_carrot-down"></i></span>
                                    <ul>
                                        <li>USD</li>
                                        <li>EUR</li>
                                        <li>USD</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="container">
                <div className="row">
                    <div className="col-lg-3 col-md-3">
                        <div className="header__logo">
                            <Link to="/">
                                <img src="/src/assets/img/client/logo.png" alt=""/>
                            </Link>
                        </div>
                    </div>
                    <div className="col-lg-6 col-md-6">
                        <HeaderMenu/>
                    </div>
                    <div className="col-lg-3 col-md-3">
                        <div className="header__nav__option">
                            <a href="#" className="search-switch"><img src="/src/assets/img/client/icon/search.png" alt=""/></a>
                            <a href="#"><img src="/src/assets/img/client/icon/heart.png" alt=""/></a>
                            <Link to="/cart">
                                <img src="/src/assets/img/client/icon/cart.png" alt=""/>
                            </Link>
                            <div className="price">${cartTotal?.toFixed(2)}</div>
                        </div>
                    </div>
                </div>
                <div className="canvas__open"><i className="fa fa-bars"></i></div>
            </div>
        </header>
        // Header Section End
    )
}

export default Header