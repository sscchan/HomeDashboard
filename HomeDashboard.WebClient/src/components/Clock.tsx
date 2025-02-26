import { useEffect, useState } from "react";

function Clock() 
{
    const [date, setDate] = useState(new Date());
    
    useEffect(() => {
        var timer = setInterval(() => setDate(new Date()), 5000 )
        return function cleanup() {
            clearInterval(timer)
        }
    });

    return(
        <div>
            <p> Time : {date.getHours()}:{date.getMinutes()}</p>
            <p> Date : {date.getDate()}/{date.getMonth() + 1}</p>

        </div>
    )
}

export default Clock;