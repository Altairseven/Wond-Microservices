<template>
  <nav class="bg-white border-gray-200 px-2 sm:px-4 py-2.5 dark:bg-gray-800">
    <div class="container flex flex-wrap justify-between items-center mx-auto">
      <a href="" class="flex items-center">
        <span
          class="
            self-center
            text-xl
            font-semibold
            whitespace-nowrap
            dark:text-white
          "
          >Wond</span
        >
      </a>
      <div
        class="
          hidden
          justify-between
          items-center
          w-full
          md:flex md:w-auto md:order-1
        "
        id="mobile-menu-2"
      >
        <ul
          class="
            flex flex-col
            mt-4
            md:flex-row md:space-x-8 md:mt-0 md:text-sm md:font-medium
          "
        >
          <li>
            <a
              href="#"
              class="
                block
                py-2
                pr-4
                pl-3
                text-white
                bg-blue-700
                rounded
                md:bg-transparent md:text-blue-700 md:p-0
                dark:text-white
              "
              aria-current="page"
              >Home</a
            >
          </li>
          <li>
            <a
              href="#"
              class="
                block
                py-2
                pr-4
                pl-3
                text-gray-700
                border-b border-gray-100
                hover:bg-gray-50
                md:hover:bg-transparent
                md:border-0
                md:hover:text-blue-700
                md:p-0
                dark:text-gray-400
                md:dark:hover:text-white
                dark:hover:bg-gray-700 dark:hover:text-white
                md:dark:hover:bg-transparent
                dark:border-gray-700
              "
              >About</a
            >
          </li>
          <li>
            <a
              href="#"
              class="
                block
                py-2
                pr-4
                pl-3
                text-gray-700
                border-b border-gray-100
                hover:bg-gray-50
                md:hover:bg-transparent
                md:border-0
                md:hover:text-blue-700
                md:p-0
                dark:text-gray-400
                md:dark:hover:text-white
                dark:hover:bg-gray-700 dark:hover:text-white
                md:dark:hover:bg-transparent
                dark:border-gray-700
              "
              >Services</a
            >
          </li>
          <li>
            <a
              href="#"
              class="
                block
                py-2
                pr-4
                pl-3
                text-gray-700
                border-b border-gray-100
                hover:bg-gray-50
                md:hover:bg-transparent
                md:border-0
                md:hover:text-blue-700
                md:p-0
                dark:text-gray-400
                md:dark:hover:text-white
                dark:hover:bg-gray-700 dark:hover:text-white
                md:dark:hover:bg-transparent
                dark:border-gray-700
              "
              >Pricing</a
            >
          </li>
          <li>
            <a
              href="#"
              class="
                block
                py-2
                pr-4
                pl-3
                text-gray-700
                border-b border-gray-100
                hover:bg-gray-50
                md:hover:bg-transparent
                md:border-0
                md:hover:text-blue-700
                md:p-0
                dark:text-gray-400
                md:dark:hover:text-white
                dark:hover:bg-gray-700 dark:hover:text-white
                md:dark:hover:bg-transparent
                dark:border-gray-700
              "
              >Contact</a
            >
          </li>
          <li>
            <button
              v-if="!authStore.user"
              type="button"
              class="inline-flex"
              @click="testLogin()"
              :disabled="authStore.loading"
            >
              <span v-if="!authStore.loading">Login</span>
              <span v-else> loading...</span>
            </button>
            <button
              v-else
              type="button"
              class="inline-flex"
              @click="testLogout()"
            >
              logout
            </button>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script lang="ts" setup>
import { useAuthStore } from "../store/Index";

const authStore = useAuthStore();

function testLogin() {
  authStore.doLogin({ username: "altairseven", password: "Test1234" });
}

function testLogout() {
  authStore.doLogout();
}
</script>
