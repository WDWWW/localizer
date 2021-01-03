import { observer } from 'mobx-react-lite'
import { Redirect, Route, RouteProps, useHistory } from 'react-router-dom'
import { useStores } from './stores/StoreProvider'

const AuthorizedRoute = observer(({ children, ...props }: RouteProps) => {
	const { authStore } = useStores()
	const history = useHistory()

	if (authStore.isLoggedIn) return <Route {...props}>{children}</Route>
	return <Redirect to={`/auth/sign-in?redirect=${history.location.pathname}`} />
})

export default AuthorizedRoute
