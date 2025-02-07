import React from 'react'

interface ArrowProps {
  onClick?: () => void;
  direction: 'next' | 'prev';
}

const SliderCustomArrow: React.FC<ArrowProps> = ({ onClick, direction }: ArrowProps): JSX.Element => {
  const arrowClass = direction === 'next' ? 'arrow_right' : 'arrow_left';

  return (
    <span className={`arrow ${arrowClass}`} onClick={onClick}></span>
  )
}

export default SliderCustomArrow