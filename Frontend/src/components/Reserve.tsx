import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useLocation, Link } from 'react-router-dom';

export interface Reservation {
  reservationId: number,
  amountOfTickets: number,
  used?: boolean
  theatreShowDateId?: number
}
// interface ReserveProps {
//     //CustomerEmail: string
//     reservation: Reservation[]
// }

const Reserve: React.FC = () => {
    const location = useLocation();
    const reservations: Reservation[] = location.state?.reservations;

    const [message, setMessage] = useState<string>(""); // Store a list of shows


    const HandleReserve = async (reservation: Reservation) => {}


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
              <hr /> {/* Add a horizontal line between reservations */}
          </div>))
          }
      </div>
      
    )
}

export default Reserve