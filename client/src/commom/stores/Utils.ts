import { autorun, set, toJS } from 'mobx'

export function autoSave(_this: any, name: string) {
	const storedJson = localStorage.getItem(name)
	if (storedJson) {
		set(_this, JSON.parse(storedJson))
	}
	autorun(() => localStorage.setItem(name, JSON.stringify(toJS(_this))))
}
