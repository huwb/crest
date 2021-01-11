.. _support:

Support
=======
As of the time of writing, the **Crest** community is extremely active
and helpful. If you need help, advice, or other forms of support head
over to our `Discord <https://discord.gg/g7GpjDC>`_ ,
`YouTube <https://www.youtube.com/channel/UC7_ZKKCXZmH64rRZqe-C0WA>`_ , or 
`Twitter <https://twitter.com/@crest_ocean>`_.

Legacy
------
* Releases
    **Crest** releases are no longer published or supported.

* Unity Versions < 2019.4.8
    Although there is a `legacy branch <https://github.com/crest-ocean/crest/tree/legacy/unity-2018>`_
    with 2018.4 support (available for download `here <https://github.com/crest-ocean/crest/archive/legacy/unity-2018.zip>`_),
    it is not actively developed nor maintained.

* **Crest** does not support OpenGL or WebGL backends.

Bugs
====
If you discover a bug with our software, please search the `Issues page <https://github.com/crest-ocean/crest/issues>`_ to see
if there is already a fix or ongoing work. If you don't find an issue which matches your  problem, please report it as a new issue.

.. note::
    See :ref:`contributing` or `CONTRIBUTING.md <https://github.com/crest-ocean/crest/blob/master/CONTRIBUTING.md>`_ for more 
    information about how to submit an issue or contribute to the project.

Known Issues
'''''''''''''
Some noteworthy known issues are:

* Sky solutions such as Azure[Sky] require some code to be added to the ocean shader for the fogging/scattering to work.
  This is a requirement of these projects and they typically come with instructions for what needs to be added. See the
  `wiki <https://github.com/crest-ocean/crest/wiki>`_ for examples.
* The built-in render pipeline version of **Crest** requires the `Draw Instanced` option on terrains to be disabled at start time.
  It can safely be re-enabled after the `Depth Cache` is populated.
