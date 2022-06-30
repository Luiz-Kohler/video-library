import React, { InputHTMLAttributes, useMemo  } from 'react';
import { MaskedInput } from 'antd-mask-input';

const InputCpfMask: React.FC<InputHTMLAttributes<HTMLInputElement>> = (props : any) => {

    const mask = useMemo(
        () => [
          {
            mask: '000.000.000-00',
            lazy: false,
          },
        ],
        []
      );

    return (
        <MaskedInput mask={mask} {...props}/>
    );
};

export default InputCpfMask;