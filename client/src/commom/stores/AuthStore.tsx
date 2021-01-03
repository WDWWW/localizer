import { makeAutoObservable } from 'mobx'
import { utils } from './Utils'

export class AuthStore {
	public accessToken: string

	constructor() {
		makeAutoObservable(this)
		this.accessToken = ''
		utils(this, 'authStore')
	}

	get isLoggedIn(): boolean {
		return !!this.accessToken
	}

	async signIn(email: string, password: string, canRefresh: boolean = true): Promise<boolean> {
		if (email === 'admin' && password === 'password') {
			this.accessToken = 'SOME_ACCESS_TOKEN'
			return true
		}

		return false
	}

	signOut() {
		this.accessToken = ''
	}
}
