import React from 'react';
import Header from './components/Header';
import {Route, Switch} from 'react-router-dom'
import SessionList from './pages/SessionList'
import SideNavigation from './components/SideNavigation'


const About = () => (
    <div>
        <h2>About</h2>
        ...
    </div>
)


function App() {
    return (
        <div className="w-full">
            <Header/>
            <main className="px-6 py-6 flex">
                <aside className="w-1/6">
                    <SideNavigation/>
                </aside>
                <div className="w-5/6 ml-4">
                    <Switch>
                        <Route path="/site/:siteId/sessions" component={SessionList}/>
                        <Route path="/site/:siteId/session/:sessionId" component={About}/>
                        <Route path="/site/:siteId/session/:sessionId/recording" component={About}/>
                        <Route path="/site/:siteId/heatmap/:page" component={About}/>
                    </Switch>
                </div>
            </main>
        </div>
    );
}

export default App;
