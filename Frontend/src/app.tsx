import React, { useState, useRef } from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Shows from './components/Shows';
import MenuBar from './components/MenuBar';
import Login from './components/Login';
import Logout from './components/Logout';
import Privacy from './components/Privacy';
import Home from './components/Home'
import AdminDashboard from './components/AdminDashboard';
import NotFound from './components/NotFound';
import CartDrawer from './components/CartDrawer';
import FavoriteIcon from './components/FavoriteIcon';
import FavoriteDrawer from './components/FavoriteDrawer';
import { CartProvider } from './components/CartContext';
import { AdminDataDTO } from './models/Admin';
import { FavoriteProvider } from './components/FavoriteContext';
import Reserve from './components/Reserve';


const App: React.FC = () => {
  const adminDataDTORef = useRef<AdminDataDTO>({
    adminId: 0,
    username: '',
    //password: '', 
    email: ''
  });
  const loginFormDataRef = useRef({username: ''});
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [showCart, setShowCart] = useState(false);
  const [showFavorites, setShowFavorites] = useState(false);

  const handleCartClick = () => setShowCart(true);
  const handleCartHide = () => setShowCart(false);

  const handleFavoritesClick = () => setShowFavorites(true);
  const handleFavoritesHide = () => setShowFavorites(false);

  //const [firstName, setFirstName] = useState("");
 // const [lastName, setLastName] = useState("");
  //const [email, setEmail] = useState("");
  return (
    <CartProvider>
      <FavoriteProvider>
        <Router>
        <MenuBar onFavoritesClick={handleFavoritesClick} isLoggedIn={isLoggedIn} onCartClick={handleCartClick} />
          <Routes>
            <Route path="/" element={<Navigate to="/Home" />} />
            <Route path="/Home" element={<Home />} />
            <Route path="/Shows" element={<Shows/>} />
            <Route path="/Login" element={<Login 
            adminDataDTORef={adminDataDTORef}
            loginFormDataRef={loginFormDataRef}
            setIsLoggedIn={setIsLoggedIn}
            />} />
            <Route path="/AdminDashboard" element={<AdminDashboard isLoggedIn={isLoggedIn}/>} />
            <Route path="/Logout" element={<Logout setIsLoggedIn={setIsLoggedIn} />} />
            <Route path="/Privacy" element={<Privacy />} />
            <Route path="/Reserve/:showDateId" element={<Reserve />} />
            <Route path="*" element={<NotFound />} /> 
          </Routes>
          <CartDrawer show={showCart} onHide={handleCartHide} />
          <FavoriteDrawer show={showFavorites} onHide={handleFavoritesHide} />
        </Router>
      </FavoriteProvider>
    </CartProvider>
  )};
  
  export default App;