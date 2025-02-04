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
    const reservations: Reservation[] = location.state?.reservation;

    const [message, setMessage] = useState<string>(""); // Store a list of shows


    const HandleReserve = async (reservation: Reservation) => {}


    return (
      <div>
        
      </div>
      
    )
}

export default Reserve