import React, { useState } from 'react';
import { useCart } from './CartContext';
import { Button, Offcanvas, ListGroup } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { Reservation } from './Reserve';
import { CartItem } from './CartContext';
import { useNavigate } from "react-router-dom";

interface CartDrawerProps {
  show: boolean;
  onHide: () => void;
}

const CartDrawer: React.FC<CartDrawerProps> = ({ show, onHide }) => {
  const navigate = useNavigate();

  const { cart, addToCart, removeFromCart, removeOneFromCart } = useCart();
  //const [reservations, setReservations] = useState<Reservation[]>([]);

  const groupedCart = cart.reduce((acc, item) => {
    const key = `${item.showDateId}-${item.showTitle}`;
    if (!acc[key]) {
      acc[key] = { ...item, count: 0 };
    }
    acc[key].count += 1;
    return acc;
  }, {} as { [key: string]: any });

  const totalPrice = Object.values(groupedCart).reduce((total, item: any) => total + item.showPrice * item.count, 0);

  const handleReservation = async () => {
    const response = await fetch("/Reservation/count")
    if (response.ok){
      var count = await response.json()

      const reservationItems: Reservation[] = Object.values(groupedCart).map((item: any) => ({
        reservationId: count += 1, // Generate a unique ID (you can use a better method for production)
        amountOfTickets: item.count,
        theatreShowDateId: item.showDateId,
        price: item.showPrice * item.count
      }));
      //setReservations(reservationItems);
      navigate("/Reserve", { state: { reservations: reservationItems, totalPrice: totalPrice } });
    }
    
  };

  return (
    <Offcanvas show={show} onHide={onHide} placement="end">
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Cart</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body className="cart-drawer-body">
        <ListGroup variant="flush">
          {Object.values(groupedCart).map((item: any, index) => (
            <ListGroup.Item key={`${item.showDateId}-${index}`}>
              <div className="d-flex justify-content-between align-items-center">
                <div>
                  <strong>{item.showTitle}</strong>
                  <p>{item.showDate}</p>
                  <p>Price: ${item.showPrice}</p>
                  <p>Quantity: {item.count}</p>
                  <div className="d-flex align-items-center">
                    <Button variant="secondary" size="sm" onClick={() => removeOneFromCart(item.showDateId)}>-</Button>
                    <span className="mx-2">{item.count}</span>
                    <Button variant="secondary" size="sm" onClick={() => addToCart(item)}>+</Button>
                  </div>
                </div>
                <Button variant="danger" onClick={() => removeFromCart(item.showDateId)}>Remove</Button>
              </div>
            </ListGroup.Item>
          ))}
        </ListGroup>
      </Offcanvas.Body>
      <div className="cart-drawer-footer">
        <h5>Total Price: ${totalPrice}</h5>
        <Button variant="success" className="w-100" disabled={cart.length === 0} onClick={handleReservation}>
          Checkout
          {/* <Link
            to="/Reserve"
            state={{ reservations }}
            style={{ color: 'inherit', textDecoration: 'none' }}
          >
            Checkout
          </Link> */}
        </Button>
      </div>
    </Offcanvas>
  );
};

export default CartDrawer;