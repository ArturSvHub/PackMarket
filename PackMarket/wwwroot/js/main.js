$(document).ready(() => {
  const onScrollAuth = () => {
    const auth = $('.auth')
    let prevscroll = $(window).scrollTop()
    let currentScroll
    $(window).scroll(() => {
      currentScroll = $(window).scrollTop()
      const hidden = () => auth.hasClass('.auth-hidden')
      if (currentScroll > prevscroll && !hidden()) {
        auth.addClass('.auth-hidden')
      }
      if (currentScroll < prevscroll && hidden()) {
        auth.removeClass('.auth-hidden')
      }
      prevscroll = currentScroll
    })
  }
  onScrollAuth()
})