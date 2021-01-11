.. _contributing:

Contributing
=============

Thank you for your interest in contributing to Crest! :)

To propose changes/additions, please fork this repository and then open a pull request.
Here are a few stylistic conventions followed in the code:

Design
---------

* **Simplicity is King**
* Modularity is favoured when it does not hurt readability or workflows.
* No dead or commented code in the master branch! (unless it is instructive)
* Tweakable/Serialized variables are private and tagged with ``SerializeField``.
* Comprehensive `XML Documentation comments <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments>`_
  are requested.
* Member variables exposed to the **Inspector GUI** require a descriptive ``Tooltip`` string.

.. note::
    Eventually, these docs will include a :ref:`source` built from your XML DocStrings.
  

Formatting
-----------

* The **Format Document** command in vanilla **VS2017** is used to apply code formatting after every edit (Ctrl+K, Ctrl+D).
* Member variables follow **lower camel case** with a single underscore prefix ``_exampleVariable``.
* Public member variables appear together before private ones in class definitions.
* A single blank line between functions and between logically separate bits of code
* Comments placed on their own line above the relevant code.

* Commit messages contain a 50 char summary line (see `git-style-guide <https://github.com/agis/git-style-guide>`_ for details).

The `git-style-guide <https://github.com/agis/git-style-guide>`_ provides additional examples of good practices that Crest aligns with.