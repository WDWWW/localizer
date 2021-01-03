import React from 'react'
import './App.css'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import HomePage from './pages/HomePage'
import AuthPage from './pages/AuthPage'
import { stores, StoresContext } from './commom/stores/StoreProvider'
import NotFoundPage from './pages/error/NotFoundPage'
import AuthorizedRoute from './commom/AuthorizedRoute'

function App() {
	return (
		<StoresContext.Provider value={stores}>
			<BrowserRouter>
				<Switch>
					<Route path="/auth" component={AuthPage} />
					<Route exact path="/not-found" component={NotFoundPage} />
					<AuthorizedRoute path="/" component={HomePage} />
				</Switch>
			</BrowserRouter>
		</StoresContext.Provider>
	)
}

export default App
