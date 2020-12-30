import React from 'react'
import './App.css'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import HomePage from './pages/HomePage'
import AuthPage from './pages/auth/AuthenticationPage'

function App() {
	return (
		<BrowserRouter>
			<Switch>
				<Route exact path="/" component={HomePage} />
				<Route path="/auth" component={AuthPage} />
			</Switch>
		</BrowserRouter>
	)
}

export default App
