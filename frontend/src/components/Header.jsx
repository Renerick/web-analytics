import React from 'react';
import logo from "../images/logo-small.svg";
import Select from 'react-select';
import {useRouteMatch, useHistory, Redirect} from "react-router-dom";
import ky from 'ky';

function Header() {
    let rootMatch = useRouteMatch("/");
    let siteMatch = useRouteMatch("/site")
    let match = useRouteMatch("/site/:siteId");
    let history = useHistory();

    const [sites, setSites] = React.useState([])

    console.log(sites)
    if (sites.length === 0) {
        ky.get("http://localhost:5000/api/v1/sites").json().then((data) => {
            setSites(data.map(i => ({value: i.siteId, label: i.name})))
        });
    }

    if (sites.length === 0) return null;

    if (rootMatch.isExact || siteMatch.isExact) {
        return (<Redirect to={"/site/" + sites[0].value + "/sessions"}/>)
    }

    if (match.isExact) {
        return (<Redirect to={"/site/" + match.params.siteId + "/sessions"}/>)
    }

    let selectedSite = sites.filter(s => s.value === match.params.siteId)[0];
    return (
        <div className="w-full flex justify-between py-4 px-6 items-stretch">
            <img src={logo} alt="Small logo"/>
            <div className="w-64">
                <Select defaultValue={selectedSite} options={sites} onChange={(data, e) => {
                    if (e.action === 'select-option') {
                        history.push("/site/" + data.value + "/sessions")
                    }
                }}></Select>
            </div>
        </div>
    );
}

export default Header;

