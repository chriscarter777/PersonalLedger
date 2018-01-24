import './css/site.css';
import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import { Layout } from './components/Layout';
import configureStore from './configureStore';
import { ApplicationState }  from './store';
import * as RoutesModule from './routes';
let layoutAndRoutes = RoutesModule.layoutAndRoutes;
let routes = RoutesModule.routes;

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = (window as any).initialReduxState as ApplicationState;
const store = configureStore(history, initialState);

// this is the original scaffolded version, which combined layout and routes into one object
//function renderApp() {
//    // This code starts up the React app when it runs in a browser.
//    ReactDOM.render(
//        <AppContainer>
//            <Provider store={ store }>
//                <ConnectedRouter history={history} children={layoutAndRoutes} />
//            </Provider>
//        </AppContainer>,
//        document.getElementById('react-app')
//    );
//}

function renderApp() {
    // This code starts up the React app when it runs in a browser.
    ReactDOM.render(
        <AppContainer>
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <Layout children={routes} />
                </ConnectedRouter>
            </Provider>
        </AppContainer>,
        document.getElementById('react-app')
    );
}

renderApp();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        layoutAndRoutes = require<typeof RoutesModule>('./routes').layoutAndRoutes;
        routes = require<typeof RoutesModule>('./routes').routes;
        renderApp();
    });
}
