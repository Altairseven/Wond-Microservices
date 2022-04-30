
import { defineStore } from "pinia";
import { LoginModel } from "../models/LoginModel";
import { LoginResponse, MapSession, Session, User } from "../models/Session";
import { http } from "../http";


export type RootState = {
    user: User | null;
    loading: boolean;
};



export const useAuthStore = defineStore({
    id: "mainStore",
    state: () =>
    ({
        session: (JSON.parse(localStorage.getItem("session")!) as Session),
        user: (JSON.parse(localStorage.getItem("user")!) as User),
        loading: false,
    } as RootState),

    actions: {
        async doLogin(credentials: LoginModel) {
            try {
                this.loading = true;
                const resp = await http.post<LoginResponse>("login", credentials);
                debugger;
                var mapped = MapSession(resp.data);
                this.user = mapped[1];
                this.loading = false;

                localStorage.setItem("session", JSON.stringify(mapped[0]));
                localStorage.setItem("user", JSON.stringify(mapped[1]));
                console.log("Session:", mapped[0]);

            } catch (error) {
                this.loading = false;
            }
        },
        doLogout() {
            localStorage.removeItem("session");
            localStorage.removeItem("user");
            this.$reset();
        }
        // createNewItem(item: Item) {
        //   if (!item) return;

        //   this.items.push(item);
        // },

        // updateItem(id: string, payload: Item) {
        //   if (!id || !payload) return;

        //   const index = this.findIndexById(id);

        //   if (index !== -1) {
        //     this.items[index] = generateFakeData();
        //   }
        // },

        // deleteItem(id: string) {
        //   const index = this.findIndexById(id);

        //   if (index === -1) return;

        //   this.items.splice(index, 1);
        // },

        // findIndexById(id: string) {
        //   return this.items.findIndex((item) => item.id === id);
        // },
    },
});
