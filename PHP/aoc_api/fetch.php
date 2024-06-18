<?php

$aocurl = 'https://adventofcode.com/';

function get_input_from_url($day, $year = 2016){
    global $aocurl;
    $session = file_get_contents('session.txt');
    
    if ($session === false) {
        echo 'Failed to read session.txt' . PHP_EOL;
        return '';
    }

    $url = $aocurl . $year . '/day/' . $day . '/input';

    $options = [
        "http" => [
            "header" => "Cookie: session=" . $session . "\r\n" .
                        "User-Agent: roland31x/php-aoc-api v1.0\r\n"
        ]
    ];

    $context = stream_context_create($options);
    $data = file_get_contents($url, false, $context);

    if ($data === false) {
        echo 'Failed to fetch input from the website.' . PHP_EOL;
        return '';
    }

    echo 'Fetched input for day ' . $day . ' from the website.' . PHP_EOL;
    echo $data . PHP_EOL;

    return $data;
}

function fetch_input($day, $year = 2016){

    $path = 'inputs/day' . $day . '.txt';
    if (file_exists($path)) {
        return file_get_contents($path);
    }
    else {
        $input = get_input_from_url($day, $year);
        file_put_contents($path, $input);
        return $input;
    }

}

?>