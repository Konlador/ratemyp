export {};

var RegexMatches: [string, RegExp][] = [
    [ 'a', /[ą]/g ],
    [ 'A', /[Ą]/g ],
    [ 'c', /[č]/g ],
    [ 'C', /[Č]/g ],
    [ 'e', /[ęė]/g ],
    [ 'E', /[ĘĖ]/g ],
    [ 'i', /[į]/g ],
    [ 'I', /[Į]/g ],
    [ 's', /[š]/g ],
    [ 'S', /[Š]/g ],
    [ 'u', /[ųū]/g ],
    [ 'U', /[ŲŪ]/g ],
    [ 'z', /[ž]/g ],
    [ 'Z', /[Ž]/g ],
];

declare global {
    interface String {
        denationalize(): string;
    }
}

String.prototype.denationalize = function(): string {
    var str = this;
    RegexMatches.forEach(pair => {
        var letter = pair["0"];
        var regex = pair["1"];
        str = str.replace(regex, letter);
    })
    return str as unknown as string
}
