import { createContext, useContext } from 'react'
import { AuthStore } from './AuthStore'

export type Stores = {
	authStore: AuthStore
}

export const stores: Stores = {
	authStore: new AuthStore(),
}

export const StoresContext = createContext<Stores | null>(null)

export function useStores(): Stores {
	const context = useContext(StoresContext)
	if (!context) throw new Error('Can not found configured context')
	return context
}
