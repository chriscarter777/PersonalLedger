import './css/site.css';
import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import { AdminLayout } from './components/AdminLayout';
import { MainLayout } from './components/MainLayout';
import configureStore from './configureStore';
import { ApplicationState }  from './store';
import * as RoutesModule from './routes';
let adminRoutes = RoutesModule.adminRoutes;
let mainRoutes = RoutesModule.mainRoutes;

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = (window as any).initialReduxState as ApplicationState;
const store = configureStore(history, initialState);

function renderApp() {
    ReactDOM.render(
        <AppContainer>
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <MainLayout children={mainRoutes} />
                </ConnectedRouter>
            </Provider>
        </AppContainer>,
        document.getElementById('react-app')
    );
}

function renderAdmin() {
    ReactDOM.render(
        <AppContainer>
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <AdminLayout children={adminRoutes} />
                </ConnectedRouter>
            </Provider>
        </AppContainer>,
        document.getElementById('react-admin')
    );
}

if (document.getElementById('react-app')) {
    renderApp();
}
if (document.getElementById('react-admin')) {
    renderAdmin();
}

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        mainRoutes = require<typeof RoutesModule>('./routes').mainRoutes;
        renderApp();
    });
}
