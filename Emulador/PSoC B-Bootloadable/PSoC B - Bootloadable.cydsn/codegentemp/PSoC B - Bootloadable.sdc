# THIS FILE IS AUTOMATICALLY GENERATED
# Project: C:\Users\Ramm\Documents\PSoC Creator\Tesis\PSoC B-Bootloadable\PSoC B - Bootloadable.cydsn\PSoC B - Bootloadable.cyprj
# Date: Tue, 14 Jul 2015 06:32:01 GMT
#set_units -time ns
create_clock -name {ADC_SAR_intClock(FFB)} -period 1000 -waveform {0 500} [list [get_pins {ClockBlock/ff_div_7}]]
create_clock -name {UART_SCBCLK(FFB)} -period 8666.6666666666661 -waveform {0 4333.33333333333} [list [get_pins {ClockBlock/ff_div_2}]]
create_clock -name {CyRouted1} -period 41.666666666666664 -waveform {0 20.8333333333333} [list [get_pins {ClockBlock/dsi_in_0}]]
create_clock -name {CyILO} -period 31250 -waveform {0 15625} [list [get_pins {ClockBlock/ilo}]]
create_clock -name {CyLFCLK} -period 31250 -waveform {0 15625} [list [get_pins {ClockBlock/lfclk}]]
create_clock -name {CyIMO} -period 41.666666666666664 -waveform {0 20.8333333333333} [list [get_pins {ClockBlock/imo}]]
create_clock -name {CyHFCLK} -period 41.666666666666664 -waveform {0 20.8333333333333} [list [get_pins {ClockBlock/hfclk}]]
create_clock -name {CySYSCLK} -period 41.666666666666664 -waveform {0 20.8333333333333} [list [get_pins {ClockBlock/sysclk}]]
create_generated_clock -name {T_Clock} -source [get_pins {ClockBlock/hfclk}] -edges {1 3 5} -nominal_period 100.26041666666666 [list [get_pins {ClockBlock/udb_div_0}]]
create_generated_clock -name {ADC_SAR_intClock} -source [get_pins {ClockBlock/hfclk}] -edges {1 25 49} [list]
create_generated_clock -name {UART_SCBCLK} -source [get_pins {ClockBlock/hfclk}] -edges {1 209 417} -nominal_period 8666.6666666666661 [list]


# Component constraints for C:\Users\Ramm\Documents\PSoC Creator\Tesis\PSoC B-Bootloadable\PSoC B - Bootloadable.cydsn\TopDesign\TopDesign.cysch
# Project: C:\Users\Ramm\Documents\PSoC Creator\Tesis\PSoC B-Bootloadable\PSoC B - Bootloadable.cydsn\PSoC B - Bootloadable.cyprj
# Date: Tue, 14 Jul 2015 06:31:58 GMT