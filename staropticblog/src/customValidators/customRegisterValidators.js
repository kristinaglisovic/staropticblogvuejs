export function FirstNameLastNameValidator(value){
    return(`/^[A-ZŠĐČĆŽa-zšđčćž+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$/`.test(value)) ? true:false
}

export function PasswordValidator(value){
    return(`/^(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčžć\d@$!%*?&]{8,15}$/`.test(value)) ? true:false
}

export function UsernameValidator(value){
    return(`/^(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])$/`.test(value)) ? true: false
}