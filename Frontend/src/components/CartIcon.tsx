import React from 'react';
import { useCart } from './CartContext';
import { Badge, Button } from 'react-bootstrap';
import { FaShoppingCart } from 'react-icons/fa';

interface CartIconProps {
  onClick: () => void;
}

const CartIcon: React.FC<CartIconProps> = ({ onClick }) => {
  const { cart } = useCart();

  return (
    <Button variant="link" onClick={onClick} style={{ position: 'relative' }}>
      <FaShoppingCart size={24} />
      {cart.length > 0 && (
        <Badge pill bg="danger" style={{ position: 'absolute', top: 0, right: 0 }}>
          {cart.length}
        </Badge>
      )}
    </Button>
  );
};

export default CartIcon;