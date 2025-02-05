import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useLocation, Link } from 'react-router-dom';

export interface Reservation {
  reservationId: number,
  amountOfTickets: number,
  used?: boolean
  theatreShowDateId?: number
  price?: number //not yet in DB
}
// interface ReserveProps {
//     //CustomerEmail: string
//     reservation: Reservation[]
// }

const Reserve: React.FC = () => {
    const location = useLocation();
    const reservations: Reservation[] = location.state?.reservations;
    const totalPrice: number = location.state?.totalPrice;

    const [message, setMessage] = useState<string>(""); // Store a list of shows


    const PostReservations = async () => {
      try {
        const response = await fetch("/Reservation/batch", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(reservations),
        });
        if (!response.ok) {
            console.log(`Error: ${await response.json()}`); 
        }
        else {
            console.log("Success:", await response.body);
            alert("Your reservations have been succesfully!")
        } 
      }
      catch (error) {
          console.error("Fetch failed:", error + JSON.stringify(reservations));
      }
    }


    return (
      <div>
        {reservations.map((reservation) => (
          <div key={reservation.reservationId}>
              <p>Reservation ID: {reservation.reservationId}</p>
              <p>Tickets: {reservation.amountOfTickets}</p>
              <p>Status: {reservation.used ? "Used" : "Not Used"}</p>
              {reservation.theatreShowDateId && (
                  <p>Show Date ID: {reservation.theatreShowDateId}</p>
              )}
              <p>Price: {reservation.price}</p>
              <hr /> {/* Add a horizontal line between reservations */}
          </div>))
          }
          <div>
            Total Price: {totalPrice}
          </div>
          <div>
            <button onClick={PostReservations}>Confirm</button>
          </div>
      </div>
      
    )
}

export default Reserve