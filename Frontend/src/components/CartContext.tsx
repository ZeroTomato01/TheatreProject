import React, { createContext, useState, useEffect, ReactNode } from 'react';

interface CartItem {
  showDateId: number;
  showTitle: string;
  showDate: string;
  showPrice: number;
}

interface CartContextProps {
  cart: CartItem[];
  addToCart: (item: CartItem) => void;
  removeFromCart: (showDateId: number) => void;
  removeOneFromCart: (showDateId: number) => void;
}

const CartContext = createContext<CartContextProps | undefined>(undefined);

export const CartProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [cart, setCart] = useState<CartItem[]>(() => {
    const savedCart = localStorage.getItem('cart');
    return savedCart ? JSON.parse(savedCart) : [];
  });

  useEffect(() => {
    localStorage.setItem('cart', JSON.stringify(cart));
  }, [cart]);

  const addToCart = (item: CartItem) => {
    setCart(prevCart => [...prevCart, item]);
  };

  const removeFromCart = (showDateId: number) => {
    setCart(prevCart => prevCart.filter(item => item.showDateId !== showDateId));
  };
  
  const removeOneFromCart = (showDateId: number) => {
    const index = cart.findIndex(item => item.showDateId === showDateId);
    if (index !== -1) {
      const newCart = [...cart];
      newCart.splice(index, 1);
      setCart(newCart);
    }
  };

  return (
    <CartContext.Provider value={{ cart, addToCart, removeFromCart, removeOneFromCart }}>
      {children}
    </CartContext.Provider>
  );
};

export const useCart = () => {
  const context = React.useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};