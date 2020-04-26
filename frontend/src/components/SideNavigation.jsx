import React from 'react';
import {Link, useRouteMatch} from 'react-router-dom';
import {Activity, Map, Settings} from 'react-feather';

function NavigationItem(props) {
    var match = useRouteMatch(props.url)

    return (
        <li className={"my-1 rounded " + (match ? "bg-primary text-white" : "hover:bg-gray-300")}>
            <Link to={props.url} className={"px-4 py-3 text-lg w-full h-full flex align-center"}>
                <span>
                    {props.icon}
                </span>
                <span className={"ml-2"}>
                {props.label}
                </span>
            </Link>
        </li>
    )
}

function SideNavigation(props) {
    let match = useRouteMatch("/site/:siteId");

    if (!match) return null;

    let siteId = match.params.siteId;
    let baseUrl = "/site/:siteId";

    let items = [
        {
            label: "Sessions",
            url: baseUrl + "/sessions",
            icon: (<Activity/>),
        },
        {
            label: "Heatmaps",
            url: baseUrl + "/heatmaps",
            icon: (<Map/>)
        },
        {
            label: "Site setting",
            url: baseUrl + "/settings",
            icon: (<Settings/>)
        },
    ]

    return (
        <nav>
            <ul>
                {items.map(i => (
                    <NavigationItem key={i.label} label={i.label} url={i.url.replace(":siteId", siteId)} icon={i.icon}/>))}
            </ul>
        </nav>
    );
}

export default SideNavigation;