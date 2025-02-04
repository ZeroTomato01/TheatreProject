import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';

import Reserve from './Reserve';

export interface TheatreShow {
  theatreShowId?: number;
  title: string;
  description: string;
  price: number;
  venue?: { name: string };
  venueId?: number
  theatreShowDateIds?: number[]
}

export interface TheatreShowDate {
  theatreShowDateId: number;
  dateAndTime: string;
  theatreShowId: number;
}

const Shows: React.FC = () => {
  const [shows, setShows] = useState<TheatreShow[]>([]);
  const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
  //maybe just add amin check, so we can use this same component in the admin dashboard?
  
  
  useEffect(() => {
    // Fetch TheatreShows
    fetch("/TheatreShow")
      .then(response => response.json())
      .then(data => setShows(data))
      .catch(error => console.error("Error fetching shows:", error));

    // Fetch TheatreShowDates separately
    fetch("/TheatreShowDate/future")
      .then(response => response.json())
      .then(data => setShowDates(data))
      .catch(error => console.error("Error fetching show dates:", error));
  }, []);

  return (
    <div>
      <h1>Shows</h1>
      <ul>
        {shows.length > 0 ? (
          shows.map(show => (
            <li key={show.theatreShowId}>
              <strong>{show.title}</strong>
              <p>{show.description}</p>
              <p><strong>Price:</strong> ${show.price}</p>
              <p><strong>Venue:</strong> {show.venue?.name}</p>

              <ul>
                {showDates
                  .filter(date => date.theatreShowId === show.theatreShowId) // Match ShowDates with Show ID
                  .map((showDate) => (
                    <li key={showDate.theatreShowDateId}>
                      <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                      <Link to={`/Reserve/${showDate.theatreShowDateId}`}> Reserve</Link>
           
                    </li>
                  ))}
              </ul>
            </li>
          ))
        ) : (
          <li>Loading shows...</li>
        )}
      </ul>
    </div>
  );
};

export default Shows;
