import React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { TheatreShow, TheatreShowDate } from './Shows';

interface adminDashboardProps {
    isLoggedIn: boolean
}

const AdminDashboard: React.FC<adminDashboardProps> = (props: adminDashboardProps) => {
    //navigate to login screen if user accessed dashboard without being logged in
    const navigate = useNavigate()
    useEffect(() => {
        if (!props.isLoggedIn) {
            navigate("/Login");
        }
    }, [props.isLoggedIn, navigate]); 

    //load in Shows and ShowDates
    const [shows, setShows] = useState<TheatreShow[]>([]);
    const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
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

    //conditional rendering of forms, Adding Shows and ShowDates
    const [showToAdd, setShowToAdd] = useState<TheatreShow>(
        {theatreShowId: 0,
        title: "",
        description: "",
        price: 0,
        theatreShowDateIds: [],
        venueId: 0
        })

    const [isAddingShow, setIsAddingShow] = useState(false)
    const toggleIsAddingShow = () => {setIsAddingShow(!isAddingShow)}

    const [showDateToAdd, setShowDateToAdd] = useState<TheatreShowDate>()
    const [isAddingShowDate, setIsAddingShowDate] = useState(false)
    const toggleIsAddingShowDate = () => {setIsAddingShowDate(!isAddingShow)}


    const AddShow = async () => {
        console.log(JSON.stringify(showToAdd))
       // try {
            await fetch("http://localhost:5097/TheatreShow", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(showToAdd)
            }).then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.log(error + "aa" + "    " + JSON.stringify(showToAdd)))



        //     if (!response.ok) {
        //         console.log(`Error: ${response.status} : ${response.statusText}`)
        //         throw new Error(`Error: ${response.status} : ${response.statusText}`);
        //     }
        //     const result = await response.json();
        //     console.log("Successfully added show:", result);
        //     // reset form after adding
        //     setShowToAdd({
        //         theatreShowId: 0,
        //         title: "",
        //         description: "",
        //         price: 0,
        //         venueId: 0,
        //         //theatreShowDateIds: []
        //     });
        // } catch (error) {
        //     console.error("Failed to add show:", error);
        // }
    };
    const EditShow = () => {}
    const DeleteShow = () => {}

    const AddShowDate = () => {}
    const EditShowDate = () => {}
    const DeleteShowDate = () => {}
    

    return (
        
        <div>
            <h1>Admin Dashboard</h1>
            <p>Shows</p>
            <button onClick={e => toggleIsAddingShow()}> {isAddingShow == false ? "Add Show" : "Cancel"} </button>
            {isAddingShow == true 
                ? <form onSubmit={AddShow}>
                    <div>
                        Title: <input 
                        value={showToAdd.title}
                        onChange={e => setShowToAdd({...showToAdd, title: e.target.value})}
                        />
                    </div>
                    <div>
                        Id: 
                        <input 
                            type="number"
                            value={showToAdd.theatreShowId === 0 ? "" : showToAdd.theatreShowId} // Show empty when it's 0
                            onChange={e => {
                                const value = e.target.value;
                                setShowToAdd({
                                    ...showToAdd,
                                    theatreShowId: value === "" ? 0 : Number(value) // Use 0 as empty value
                                });
                            }}
                        />
                    </div>
                    <div>
                        Description: <input 
                        value={showToAdd.description}
                        onChange={e => setShowToAdd({...showToAdd, description: e.target.value})}
                        />
                    </div>
                    <div>
                        Price: 
                        <input 
                            type="number"
                            value={showToAdd.price === 0 ? "" : showToAdd.price} // Show empty when it's 0
                            onChange={e => {
                                const value = e.target.value;
                                setShowToAdd({
                                    ...showToAdd,
                                    price: value === "" ? 0 : Number(value) // Use 0 as empty value
                                });
                            }}
                        />
                    </div>
                    <div>
                        VenueId: 
                        <input 
                            type="number"
                            value={showToAdd.venueId === 0 ? "" : showToAdd.venueId} // Show empty when it's 0
                            onChange={e => {
                                const value = e.target.value;
                                setShowToAdd({
                                    ...showToAdd,
                                    venueId: value === "" ? 0 : Number(value) // Use 0 as empty value
                                });
                            }}
                        />
                    </div>
                    <div>
                        TheatreShowDateIds (input like 1, 2, 3): <input
                        value={showToAdd.theatreShowDateIds?.join(", ") + ", "}
                        onChange={e => {
                            const newArray = e.target.value
                                .split(",")
                                .map(num => num.trim())
                                .filter(num => num !== "")
                                .map(Number);
                            
                            setShowToAdd({...showToAdd, theatreShowDateIds: newArray});
                        }} 
                        />
                    </div>
                    <button> Submit </button>

                </form> 
                : ""}
            <form></form>
            <ul>
            {shows.length > 0 ? (
                shows.map(show => (
                <li key={show.theatreShowId}>
                    <strong>{show.title}     </strong> 
                    <button onClick={EditShow}> Edit Show </button>
                    <button onClick={DeleteShow}> Delete Show </button>
                    <p>{show.description}</p>
                    <p><strong>Price:</strong> ${show.price}</p>
                    <p><strong>Venue:</strong> {show.venue?.name}</p>
                  
                    <button onClick={AddShowDate}> Add ShowDate</button>
                    <ul>
                    {showDates
                        .filter(date => date.theatreShowId === show.theatreShowId) // Match ShowDates with Show ID
                        .map((showDate) => (
                        <li key={showDate.theatreShowDateId}>
                            <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString() + "     "}   
                            <button onClick={EditShowDate}> Edit ShowDate </button>
                            <button onClick={DeleteShowDate}> Delete ShowDate </button>
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

export default AdminDashboard;