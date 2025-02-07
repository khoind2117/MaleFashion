import { NavLink } from "react-router-dom";

const HeaderMenu = () => {
  return (
    <nav className="header__menu mobile-menu">
      <ul>
        <li>
          <NavLink
            to="/"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Home
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/shop"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Shop
          </NavLink>
        </li>
        <li>
          <a href="#">Pages</a>
          <ul className="dropdown">
            <li>
              <NavLink
                to="/about"
                className={({ isActive }) => (isActive ? "active" : "")}
              >
                About Us
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/shop-details"
                className={({ isActive }) => (isActive ? "active" : "")}
              >
                Shop Details
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/shopping-cart"
                className={({ isActive }) => (isActive ? "active" : "")}
              >
                Shopping Cart
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/checkout"
                className={({ isActive }) => (isActive ? "active" : "")}
              >
                Check Out
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/blog-details"
                className={({ isActive }) => (isActive ? "active" : "")}
              >
                Blog Details
              </NavLink>
            </li>
          </ul>
        </li>
        <li>
          <NavLink
            to="/blog"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Blog
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/contact"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Contacts
          </NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default HeaderMenu;
