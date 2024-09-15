import PropTypes from 'prop-types'
import { PropsWithChildren, useEffect } from 'react'

// ==============================|| NAVIGATION SCROLL TO TOP ||============================== //

const NavigationScroll = (props: PropsWithChildren) => {
  useEffect(() => {
    window.scrollTo({
      top: 0,
      left: 0,
      behavior: 'smooth',
    })
  }, [])

  return props.children || null
}

NavigationScroll.propTypes = {
  children: PropTypes.node,
}

export default NavigationScroll
