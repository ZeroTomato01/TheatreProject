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
    // useEffect(() => {
    // // Fetch TheatreShows
    // fetch("/TheatreShow")
    //     .then(response => response.json())
    //     .then(data => setShows(data))
    //     .catch(error => console.error("Error fetching shows:", error));

    // // Fetch TheatreShowDates separately
    // fetch("/TheatreShowDate/future")
    //     .then(response => response.json())
    //     .then(data => setShowDates(data))
    //     .catch(error => console.error("Error fetching show dates:", error));
    // }, []);

        // Function to fetch and update the shows list
        const fetchShows = async () => {
            try {
                const response = await fetch("/TheatreShow");
                if (!response.ok) {
                    throw new Error(`Failed to fetch shows: ${response.statusText}`);
                }
                const data = await response.json();
                setShows(data); // Update state with fetched shows
            } catch (error) {
                console.error("Error fetching shows:", error);
            }
        };

        const fetchShowDates = async () => {
            try {
                const response = await fetch("/TheatreShowDate/future");
                if (!response.ok) {
                    throw new Error(`Failed to fetch ShowDates: ${response.statusText}`);
                }
                const data = await response.json();
                setShowDates(data); // Update state with fetched shows
                console.log(showDates)
            } catch (error) {
                console.error("Error fetching shows:", error);
            }
        };

        // Fetch shows when the component mounts
        useEffect(() => {
            fetchShows();
            fetchShowDates();
        }, []);
    

    //conditional rendering of forms, Adding Shows and ShowDates
    const [showToAdd, setShowToAdd] = useState<TheatreShow>(
        {theatreShowId: 0,
        title: "",
        description: "",
        price: 0,
        theatreShowDateIds: [],
        venueId: 0,
        venue: undefined
        })

    const [isAddingShow, setIsAddingShow] = useState(false)
    const toggleIsAddingShow = () => {setIsAddingShow(!isAddingShow)}

    const [editedShow, setEditedShow] = useState<TheatreShow>({...showToAdd}) //makes shallow copy with default values
    const [isEditingShow, setIsEditingShow] = useState(false)
    const toggleIsEditingShow = (show: TheatreShow) => {
        setIsEditingShow(!isEditingShow);
        if (!isEditingShow) {
          setEditedShow({ ...show }); // Set the edited show only when entering edit mode
        }
      };

    const [showDateToAdd, setShowDateToAdd] = useState<TheatreShowDate>()
    const [isAddingShowDate, setIsAddingShowDate] = useState(false)
    const toggleIsAddingShowDate = () => {setIsAddingShowDate(!isAddingShow)}


    const AddShow = async (e: React.FormEvent<HTMLFormElement>) => { 
        e.preventDefault();
        console.log(JSON.stringify(showToAdd))
      
            // await fetch("http://localhost:5097/TheatreShow", {
            //     method: "POST",
            //     headers: {
            //         "Content-Type": "application/json",
            //     },
            //     body: JSON.stringify(showToAdd)
            // }).then(response => response.json())
            // .then(data => console.log(data))
            // .catch(error => console.log(error + "aa" + JSON.stringify(showToAdd)))

    
        console.log("Show data being sent:", JSON.stringify(showToAdd));
    
        try {
            const response = await fetch("/TheatreShow", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(showToAdd),
            });
    
            if (!response.ok) {
                console.log(`Error: ${response.status} - ${response.statusText}`);
                const errorData = await response.json();
                console.log("Error details:", errorData);
            } else {
                const data = await response.json();
                console.log("Success:", data);
            }
        } catch (error) {
            console.error("Fetch failed:", error + JSON.stringify(showToAdd));
        }
    };

    const EditShow = async (show: TheatreShow) => {
        if(window.confirm("Are you sure you want te edit this show?")) {
            try {
                const response = await fetch("/TheatreShow", {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(editedShow),
                });
        
                if (!response.ok) {
                    console.log(`Error: ${response.status} - ${response.statusText}`);
                    const errorData = await response.json();
                    console.log("Error details:", errorData);
                } else {
                    const data = await response.json();
                    console.log("Success:", data);
                }
            } catch (error) {
                console.error("Fetch failed:", error + JSON.stringify(editedShow));
            }
        }
        else
            alert("canceled")
    }
    const DeleteShow = async (show: TheatreShow) => {
        if(window.confirm("Are you sure you want te delete this show?")) {
            try {
                const response = await fetch(`/TheatreShow?id=${show.theatreShowId}`, {
                    method: "DELETE",
                });
        
                if (!response.ok) {
                    throw new Error(`Failed to delete show: ${response.statusText}`);
                }
                console.log("Show deleted successfully");
            } catch (error) {
                console.error("Error deleting show:", error);
            }
            fetchShows()
            fetchShowDates()
            alert("show deleted")}
            
        else
            alert("canceled")
        
    };

    const AddShowDate = () => {}
    const EditShowDate = () => {}
    const DeleteShowDate = () => {}
    

    return (
        
        <div>
            <h1>Admin Dashboard</h1>
            <button onClick={e => console.log(showDates)}> debug button</button>
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
                            onChange={e => setShowToAdd({...showToAdd, price: e.target.valueAsNumber})}/>
                    </div>
                    <div>
                        VenueId: 
                        <input 
                            type="number"
                            value={showToAdd.venueId === 0 ? "" : showToAdd.venueId} // Show empty when it's 0
                            onChange={e => setShowToAdd({...showToAdd, venueId: e.target.valueAsNumber})}/>
                    </div>
                    {/* <div>
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
                    </div> */}
                    <button> Submit </button>

                </form> 
                : ""}
            <ul>
            {shows.length > 0 ? (
                shows.map(show => (
                <li key={show.theatreShowId}>
                    <strong>{show.title}     </strong> 
                    {/* <button onClick={e => EditShow}> Edit Show </button> */}
                    
                    <button onClick={e => toggleIsEditingShow(show)}> {isEditingShow == true && editedShow.theatreShowId == show.theatreShowId ? "Cancel" : "Edit Show"} </button>
                        {isEditingShow == true && editedShow.theatreShowId == show.theatreShowId ? 
                        
                        <form onLoad={e=> setEditedShow({...show})}
                        onSubmit={e=> {e.preventDefault(); EditShow(editedShow);}}>
                            <div>
                                Title:
                                <input
                                    value ={editedShow.title}
                                    onChange={e => setEditedShow({...editedShow, title: e.target.value})} />
                            </div>
                            <div>
                                Description:
                                <input
                                    value ={editedShow.description}
                                    onChange={e => setEditedShow({...editedShow, description: e.target.value})} />
                            </div>
                            <div>
                                Price: 
                                <input 
                                    type="number"
                                    value={editedShow.price === 0 ? "" : editedShow.price} // Show empty when it's 0
                                    onChange={e => setEditedShow({...editedShow, price: e.target.valueAsNumber})}/>
                            </div>
                            <div>
                                VenueId: 
                                <input 
                                    type="number"
                                    value={editedShow.venueId === 0 ? "" : editedShow.venueId} // Show empty when it's 0
                                    onChange={e => setEditedShow({...editedShow, venueId: e.target.valueAsNumber})}/>
                                {/* VenueId: 
                                <input 
                                    type="number"
                                    value={showToAdd.venueId === 0 ? "" : showToAdd.venueId} // Show empty when it's 0
                                    onChange={e => {
                                        const value = e.target.value;
                                        setShowToAdd({
                                            ...showToAdd,
                                            venueId: value === "" ? 0 : Number(value) // Use 0 as empty value
                                        });
                                    }} */}
                            </div>
                            {/* <div>
                                TheatreShowDateIds (input like 1, 2, 3): <input
                                value={editedShow.theatreShowDateIds?.join(", ")}
                                onChange={e => {
                                    const newArray = e.target.value
                                        .split(",")
                                        .map(num => num.trim())
                                        .filter(num => num !== "")
                                        .map(Number);
                                    
                                    setShowToAdd({...editedShow, theatreShowDateIds: newArray});
                                }} />
                            </div> */}
                            <button> Submit </button>
                        </form>
                        : ""
                        
                    }
                    <button onClick={e => DeleteShow(show)}> Delete Show </button>
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