import { useEffect, useState } from "react";

interface StatusVersionResponse {
    version: String;
    deploymentDateTime: Date;
}

function DashboardReloader() 
{
    const DATA_FETCH_INTERVAL_IN_MINUTES : number = 0.2; 

    const [serverPreviousDeploymentDateTime, setServerPreviousDeploymentDateTime] = useState<Date>(new Date(8640000000000000));
    const [serverDeploymentDateTime, setServerDeploymentDateTime] = useState<Date>(new Date(8640000000000000));

    useEffect(() => {
        async function fetchServerDeploymentDateTime() {
            const response = await fetch("/api/Status/Version")
            var statusVersionResponse : StatusVersionResponse = await response.json();

            setServerDeploymentDateTime(new Date(statusVersionResponse.deploymentDateTime));
        }

        // Initial data population
        fetchServerDeploymentDateTime();

        // Scheduled data refresh
        var timer = setInterval(() => fetchServerDeploymentDateTime(), DATA_FETCH_INTERVAL_IN_MINUTES * 60 * 1000 )
        return function cleanup() {
            clearInterval(timer)
        }
    }, []);

    useEffect(() => {
        if (serverPreviousDeploymentDateTime < serverDeploymentDateTime)
        {
            console.log("Backend server restart detected. Reloading dashboard.")
            window.location.reload();
        }
        setServerPreviousDeploymentDateTime(new Date(serverDeploymentDateTime))
    }, [serverDeploymentDateTime])

    const divStyle = {
        fontSize: '1.0vw',
        textAlign: "right" as const
    }

    return (
        <div style={divStyle}>
            
            Server Deployed at: {serverDeploymentDateTime.toString()}
        </div>
    )
}

export default DashboardReloader;