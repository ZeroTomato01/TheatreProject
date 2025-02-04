import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './styles.css';
import CartIcon from './CartIcon'

interface MenuBarProps {
    isLoggedIn: boolean
    onCartClick: () => void;
};

const MenuBar: React.FC<MenuBarProps> = ({isLoggedIn, onCartClick}) => {
    return (
      <nav className = 'menu-bar'>
      <div className="menu-bar-content">
        <div className="menu-bar-left">
          <h1>BerserkerCinema</h1>
        </div>
        <div className="menu-bar-right">
          <CartIcon onClick={onCartClick} />
        </div>
      </div>
        <ul>
          <li><Link to='/Home'>Home</Link></li>
          <li><Link to='/Shows'>Shows</Link></li>
          <li><Link to='/Privacy'>Privacy</Link></li>
          {isLoggedIn ? (<>
              <li><Link to='/Profile'>Profile</Link></li>
              <li><Link to='/AdminDashboard'>AdminDashboard</Link></li>
              <li><Link to='/Logout'>Logout</Link></li>
            </>) 
            : 
            (<>
            <li><Link to='/Login'>Login</Link></li>
            </>)
          }
        </ul>
      </nav>
    );
  };
  
  export default MenuBar;