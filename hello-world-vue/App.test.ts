import { mount } from '@vue/test-utils'
import App from '@/App.vue'

describe('App.vue', () => {
  it('renders You did it! text', () => {
    const wrapper = mount(App)
    expect(wrapper.text()).toContain('You did it!') // Update this according to your actual content in App.vue
  })
})
