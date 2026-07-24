refactor the aggregator for implementing jitter
extract method from the method,
and add the sort and timeing code inside the jitter,
and simply the aggregator and remove the event handler
Sorted etc...

so:

public event EventHandler<RtpSequenceEventArgs> SequenceSorting;

public event EventHandler<RtpSequenceEventArgs> SequenceSorted;

these event will be moved when the jitter