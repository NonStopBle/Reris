<template>
  <q-page class="row items-center justify-evenly" style="background-color: #f5f5f5;">
    <div>
      <div class="column items-center no-select" style="height: 150px;">
        <div class="flex text-center">
          <div class="login-header-title-label no-warp red-hat-normal-font">RERIS</div>
        </div>
        <div class="flex text-center">
          <div class="login-header-subtitle-label red-hat-normal-font">Robotics Interface System</div>
        </div>
      </div>
      <div v-if="isVerified">
        <div class="column text-center items-center" style="height: 70px;">
        <div v-if="qrCode" class="flex text-center">
          <img :src="qrCode" alt="QR Code" />
        </div>
      </div>
      <br>
      <br>
      <br>
      </div>
      
      <div class="column" style="height: 70px;">
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Security Code</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input v-model="secureCode" rounded outlined bg-color="white" />
          </div>
        </div>
      </div>
      <br>

      <div class="column items-center" style="height: 150px;">
        <div class="login-header-forget-label red-hat-normal-font">
          <div>Forgot password?</div>
        </div>
        <br>

        <!-- Show the "Verify" button initially -->
        <div v-if="!isVerified">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="" label="Verify"
            style="width: 100px; background-color: #4BCC53;" @click="verifySecureCode" />
        </div>

        <!-- Show the "Go" button after the correct code is entered -->
        <div v-if="isVerified">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="" label="Go"
            style="width: 100px; background-color: #4BCC53;" @click="goToNextPage" />
        </div>
      </div>
      <br>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import QRCode from 'qrcode';

const secureCode = ref('');
const qrCode = ref<string | null>(null);
let securecode_chunk = '';
const isVerified = ref(false); // Track whether the secure code has been verified
const router = useRouter();

onMounted(async () => {
  const token = localStorage.getItem('authToken'); // Get token from localStorage
  const username = localStorage.getItem('username'); // Get username from localStorage

  if (!token || !username) {
    alert('User not found!');
    return;
  }

  const response = await axios.post(
    'http://localhost:5184/api/auth/secure/code',
    { username },
    { headers: { Authorization: `Bearer ${token}` } } // Pass token as Bearer token
  );

  if (response.data && response.data.securecode) {
    securecode_chunk = response.data.securecode;
  }
});

// Verify the secure code entered by the user
const verifySecureCode = async () => {
  try {
    // Check if the secure code matches
    if (securecode_chunk === secureCode.value) {
      // Generate QR code for secure code
      qrCode.value = await QRCode.toDataURL(securecode_chunk);
      
      // Set verification state to true
      isVerified.value = true;
      console.log('Secure code matched and QR code generated.');
    } else {
      alert('Incorrect secure code!');
    }
  } catch (error) {
    console.error('Error verifying secure code:', error);
    alert('Verification failed!');
  }
};

// Navigate to the next page after the secure code is verified
const goToNextPage = async () => {
  try {
    // Redirect to the desired page (e.g., dashboard)
    await router.push('/'); // Replace '/next_page' with the actual route
  } catch (error) {
    console.error('Navigation error:', error);
  }
};
</script>
