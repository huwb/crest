.. _installation:

Installation
============

Pre-Requisites
---------------
* Unity Version:
    * Each release on GitHub specifies which version of Unity3D it was developed on.

        * Currently, **Unity 2019.4.8** is the minimum version.
    * The Standard Render Pipeline (SRP) assets on the Asset Store specify their minimum version required.
* **.NET 4.x Runtime**
* Shader Compilation Target 4.5 or above
    * Unfortunately, **Crest** does not currently support OpenGL or WebGL backends.
* To Use **Crest** Example Content:
    * A layer named *Terrain* must be present in your projects.
    * Post processing package may be present (used for aesthetic reasons).

    .. tip::
        If the post processing package is not installed you will see an unassigned script. Simply
        remove the script to continue.

Steps
-----
To install **Crest**:

#. Download the latest version:

    * Pick a release from the `Releases page <https://github.com/huwb/crest-oceanrender/releases>`_ **OR**
    * Fork/clone the repository located on `GitHub <https://github.com/crest-ocean/crest>`_ **OR**
    * Download the latest :file:`master.zip` from `here <https://github.com/crest-ocean/crest/archive/master.zip>`_

#. Copy the files located in :file:`crest/Assets/Crest` into an existing Unity3D project.
#. Ensure that the **Color Space** is set to **Linear** (:menuselection:`Edit --> Project Settings --> Player --> Other Settings --> Rendering --> Color Space`).
#. Ensure that the lighting has been generated (:menuselection:`Window --> Rendering --> Lighting Settings --> Debug Settings --> Generate Lighting`)

.. note::
    The directory :file:`crest/Assets/Crest/Crest-Examples` contains example content that is often useful for first time users but not
    required for the core **Crest** functionality.

    Similarly, :file:`crest/Assets/Development` is also not needed as it is only for **Crest** development.

Conclusion
----------
Perfect! Now **Crest** is installed in your project. For information on how to setup the ocean to function properly in your scene, head to :ref:`quickstart`. 